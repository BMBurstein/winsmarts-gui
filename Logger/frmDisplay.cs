using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Logger
{
	public partial class frmDisplay : Form
	{
		List<Task> tasks = new List<Task>();
		int displayUntil;
		bool activeMode;

		private List<LogEntry> log_;
		public List<LogEntry> log
		{
			get { return log_; }
			private set
			{
				if (value == null)
					log_ = new List<LogEntry>();
				else
					log_ = value;
				lsvLog.Items.Clear();
				foreach (var item in log)
				{
					addToLog(item);
				}
				refill();
			}
		}

		public frmDisplay(List<LogEntry> log = null, bool active = true)
		{
			InitializeComponent();
			activeMode = active;
			Clear(log);
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
			btnPause_Click(btnPause, null);
			tabsViews.SelectedIndex = 1;
			log = newLog;
		}

		internal void refill()
		{
			ganttChart.Clear();
			lsvTasks.Items.Clear();

			if (!btnPause.Checked)
				displayUntil = log.Count;
			if (displayUntil == 0)
				return;

			displayUntil--;
			for (int i = 0; i < displayUntil; i++)
			{
				handleMsg(log[i], false);
			}
			while (displayUntil < log.Count && !handleMsg(log[displayUntil++], false))
				;
		}

		internal bool handleMsg(LogEntry entry, bool buildLog = true)
		{
			if (buildLog)
			{
				log.Add(entry);
				addToLog(entry);
				if (btnPause.Checked)
					return false;
			}

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
				case LogMsg.LOG_CONTEXT_SWITCH_ON:
				case LogMsg.LOG_CONTEXT_SWITCH_OFF:
				case LogMsg.LOG_TIMER:
				default:
					return false;
			}

			return true;
		}

		private void addToLog(LogEntry entry)
		{
			ListViewItem item = new ListViewItem();
			item.Text = entry.num.ToString();
			item.SubItems.Add(entry.msg.ToString());
			item.SubItems.Add(String.Join(" | ", entry.props));
			lsvLog.Items.Add(item);
			if (!btnPause.Checked)
				item.EnsureVisible();
		}

		private void handleContextSwitch(LogEntry entry)
		{
			ganttChart.addSlice(uint.Parse(entry.props[0]));
		}

		private void handleDeclareTask(LogEntry entry)
		{
			Task task = new Task() { tid = uint.Parse(entry.props[0]), name = entry.props[1], priority = uint.Parse(entry.props[2]), state = TaskStates.READY };
			tasks.Add(task);

			ListViewItem item = new ListViewItem(entry.props.ToArray());
			if (item.SubItems[2].Text == uint.MaxValue.ToString())
				item.SubItems[2].Text = "MIN";
			item.SubItems.Add("Ready");
			lsvTasks.Items.Add(item);

			ganttChart.addTask(task.tid, task.name);
		}

		private void handleStatusChanged(LogEntry entry)
		{
			foreach (ListViewItem item in lsvTasks.Items)
			{
				if (item.SubItems[0].Text == entry.props[0])
				{
					item.SubItems[3].Text = entry.props[1];
					break;
				}
			}
		}

		private void btnPause_Click(object sender, EventArgs e)
		{
			btnSkipStart.Enabled =
				btnStepB.Enabled =
				btnStepF.Enabled =
				btnSkipEnd.Enabled = btnPause.Checked;
		}

		private void btnSkipStart_Click(object sender, EventArgs e)
		{
			displayUntil = 0;
			refill();
		}

		private void btnSkipEnd_Click(object sender, EventArgs e)
		{
			displayUntil = log.Count;
			refill();
		}

		private void btnStepB_Click(object sender, EventArgs e)
		{
			if (displayUntil > 0)
				displayUntil--;
			refill();
		}

		private void btnStepF_Click(object sender, EventArgs e)
		{
			if (displayUntil < log.Count)
				displayUntil++;
			refill();
		}
	}
}
