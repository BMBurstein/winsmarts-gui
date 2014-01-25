using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Collections.ObjectModel;

namespace Logger
{
	public partial class frmDisplay : Form
	{
		static public frmDisplay activeDisplay;
		Dictionary<uint, Task> tasks = new Dictionary<uint, Task>();
		int displayUntil = 0;
		bool activeMode;
		UdpClient udpc;

		public ObservableCollection<LogEntry> log { get; private set; }

		public frmDisplay(ObservableCollection<LogEntry> log = null, bool active = true)
		{
			InitializeComponent();
			lsvLog.DoubleBuffered(true);

			tabsViews_Resize(this, null);

			activeMode = active;
			if (activeMode)
			{
				udpc = new UdpClient();
				udpc.Connect("localhost", 44558);
				udpc.Client.ReceiveTimeout = 5000;
			}

			if (log == null)
				this.log = new ObservableCollection<LogEntry>();
			else
				this.log = log;
			this.log.CollectionChanged += log_CollectionChanged;

			Clear();
		}

		void log_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
					foreach (LogEntry item in e.NewItems)
					{
						handleMsg(item);
					}
					break;
				case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
					lsvLog.Items.Clear();
					foreach (LogEntry item in log)
					{
						addToLog(item);
					}
					refreshAllViews();
					break;
				default:
					break;
			}
		}

		public void Clear(List<LogEntry> newLog = null)
		{
			if (!activeMode)
			{
				btnPause.Checked = true;
				btnPause.Enabled = false;
				displayUntil = newLog.Count;
			}
			else
			{
				btnPause.Checked = false;
				btnPause.Enabled = true;
			}
			setToolbarState();
			tabsViews.SelectedIndex = 1;
			tasks.Clear();
			refreshAllViews();
		}

		private void refreshAllViews(ROUND round = ROUND.ROUND_UP)
		{
			ganttChart.Clear();
			lsvTasks.Items.Clear();

			if (!btnPause.Checked || displayUntil > log.Count)
				displayUntil = log.Count;
			else if (displayUntil < 0)
				displayUntil = 0;

			if (log.Count == 0)
				return;

			int displayed = -1;
			for (int i = 0; i < displayUntil; i++)
			{
				if (updateDisplay(i))
					displayed = i;
			}

			if (round == ROUND.ROUND_UP && displayUntil != displayed + 1)
			{
				while (displayUntil < log.Count && !updateDisplay(displayUntil))
				{
					displayUntil++;
				}
				displayUntil++;
			}
			else if (round == ROUND.ROUND_DOWN)
			{
				displayUntil = displayed + 1;
			}

			for (int i = displayUntil; i < lsvLog.Items.Count; i++)
			{
				lsvLog.Items[i].BackColor = Color.Transparent;
			}

			if (displayUntil > 0)
				lsvLog.EnsureVisible(displayUntil - 1);
			else
				lsvLog.EnsureVisible(0);
		}

		internal void handleMsg(LogEntry entry)
		{
			int i = addToLog(entry);
			if (!btnPause.Checked)
			{
				updateDisplay(i);
				lsvLog.EnsureVisible(i);
				displayUntil = i + 1;
			}
		}

		private bool updateDisplay(int index)
		{
			LogEntry entry = log[index];

			lsvLog.Items[index].BackColor = Color.LightGreen;

			switch (entry.msg)
			{
				case LogMsg.LOG_NEW_TASK:
					handleDeclareTask(entry);
					break;
				case LogMsg.LOG_CONTEXT_SWITCH:
					handleContextSwitch(entry);
					break;
				case LogMsg.LOG_TASK_STATUS_CHANGE:
					handleStatusChanged(entry);
					break;
				case LogMsg.LOG_TASK_PROP_SET:
					handlePropSet(entry);
					break;
				case LogMsg.LOG_DEADLOCK:
					handleEnd("DEADLOCK!", true);
					break;
				case LogMsg.LOG_END:
					handleEnd("Done!", false);
					break;
				case LogMsg.LOG_TIME_OUT:
					handleEnd("Time out!", false);
					break;
				case LogMsg.LOG_REDECLARE:
				case LogMsg.LOG_CONTEXT_SWITCH_ON:
				case LogMsg.LOG_CONTEXT_SWITCH_OFF:
				case LogMsg.LOG_TIMER:
				default:
					return false;
			}

			return true;
		}

		private void handleEnd(string msg, bool error)
		{
			activeDisplay = null;
			activeMode = false;
			btnPause.Checked = true;
			btnPause.Enabled = false;
			setToolbarState();
			MessageBox.Show("End of session", msg, MessageBoxButtons.OK, error ? MessageBoxIcon.Error : MessageBoxIcon.Information);
		}

		private void handlePropSet(LogEntry entry)
		{
			tasks[uint.Parse(entry.props[0])].props[(TaskProps)int.Parse(entry.props[1])] = entry.props[2];
		}

		private int addToLog(LogEntry entry)
		{
			ListViewItem item = new ListViewItem();
			item.Text = entry.num.ToString();
			item.SubItems.Add(entry.msg.ToString());

			if (entry.msg == LogMsg.LOG_TASK_STATUS_CHANGE)
			{
				string[] statusChangeDetials = new string[] { entry.props[0], ((TaskStates)int.Parse(entry.props[1])).ToString() };
				item.SubItems.Add(String.Join(" | ", statusChangeDetials));
			}
			else
				item.SubItems.Add(String.Join(" | ", entry.props));

			lsvLog.Items.Add(item);

			return item.Index;
		}

		private void handleContextSwitch(LogEntry entry)
		{
			//ganttChart.changeState(uint.Parse(entry.props[0]), TaskStates.RUNNING);
		}

		private void handleDeclareTask(LogEntry entry)
		{
			Task task = new Task() {
				tid = uint.Parse(entry.props[0]),
				name = entry.props[1],
				priority = uint.Parse(entry.props[2]),
				state = TaskStates.READY,
				props = new Dictionary<TaskProps,object>()
			};
			tasks.Add(task.tid, task);

			ListViewItem item = new ListViewItem(entry.props.ToArray());
			if (item.SubItems[2].Text == uint.MaxValue.ToString())
				item.SubItems[2].Text = "MIN";
			item.SubItems.Add("Ready");
			item.Tag = task;
			lsvTasks.Items.Add(item);

			ganttChart.addTask(task.tid, task.name);
		}

		private void handleStatusChanged(LogEntry entry)
		{
			TaskStates state = (TaskStates)int.Parse(entry.props[1]);
			foreach (ListViewItem item in lsvTasks.Items)
			{
				if (item.SubItems[0].Text == entry.props[0])
				{
					item.SubItems[3].Text = state.ToString();
					ganttChart.changeState(uint.Parse(entry.props[0]), state, state == TaskStates.RUNNING);
					break;
				}
			}
		}

		private static readonly byte[] pauseMsg = new byte[] { (byte)DEBUG_COMMANDS.PAUSE };
		private static readonly byte[] resumeMsg = new byte[] { (byte)DEBUG_COMMANDS.CONTINUE };
		private static readonly byte[] stepMsg = new byte[] { (byte)DEBUG_COMMANDS.STEP };

		private void btnPause_Click(object sender, EventArgs e)
		{
			bool ret;
			if (btnPause.Checked)
				ret = sendCommand(pauseMsg);
			else
				ret = sendCommand(resumeMsg);

			if (!ret)
				btnPause.Checked = !btnPause.Checked;
			else
			{
				setToolbarState();
				if (!btnPause.Checked)
					refreshAllViews();
			}
		}

		private void setToolbarState()
		{
			btnSkipStart.Enabled =
				btnStepB.Enabled =
				btnStepF.Enabled =
				btnSkipEnd.Enabled =
				btnStepRun.Enabled = btnPause.Checked;
		}

		private void btnSkipStart_Click(object sender, EventArgs e)
		{
			displayUntil = 0;
			refreshAllViews();
		}

		private void btnSkipEnd_Click(object sender, EventArgs e)
		{
			displayUntil = log.Count;
			refreshAllViews();
		}

		private void btnStepB_Click(object sender, EventArgs e)
		{
			displayUntil--;
			refreshAllViews(ROUND.ROUND_DOWN);
		}

		private void btnStepF_Click(object sender, EventArgs e)
		{
			displayUntil++;
			refreshAllViews();
		}

		private void btnStepRun_Click(object sender, EventArgs e)
		{
			sendCommand(stepMsg);
		}

		private bool sendCommand(byte[] command)
		{
			try
			{
				IPEndPoint ipep = null;
				byte[] ret;

				udpc.Send(command, command.Length);
				ret = udpc.Receive(ref ipep);
				if (ret[0] != 1)
					throw new Exception("Error processing request");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
			return true;
		}

		private void lsvLog_ItemActivate(object sender, EventArgs e)
		{
			displayUntil = lsvLog.SelectedIndices[0] + 1;
			refreshAllViews(ROUND.DONT_ROUND);
		}

		private void tabsViews_Resize(object sender, EventArgs e)
		{
			int x = (lsvLog.Width - lsvLog.Columns[0].Width) / 3 == 0 ? 1 : (lsvLog.Width - lsvLog.Columns[0].Width) / 3;
			lsvLog.Columns[1].Width = x;
			lsvLog.Columns[2].Width = x * 2 - 22;

			x = (lsvTasks.Width - lsvTasks.Columns[0].Width) / 7 == 0 ? 1 : (lsvTasks.Width - lsvTasks.Columns[0].Width) / 7;
			lsvTasks.Columns[1].Width = x * 2;
			lsvTasks.Columns[2].Width = x;
			lsvTasks.Columns[3].Width = x * 4 - 10;
		}

		private void lsvTasks_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lsvTasks.SelectedItems.Count > 0)
			{
				Task task = (Task)lsvTasks.SelectedItems[0].Tag;

				propGrid.SelectedObject = new DictionaryPropertyGridAdapter(task.props);
			}
		}

		private enum ROUND
		{
			ROUND_UP,
			ROUND_DOWN,
			DONT_ROUND,
		}
	}
}
