﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Logger
{
	public partial class frmDisplay : Form
	{
		List<Task> tasks = new List<Task>();
		int displayUntil = 0;
		bool activeMode;
		UdpClient udpc;


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
				refresh();
			}
		}

		public frmDisplay(List<LogEntry> log = null, bool active = true)
		{
			InitializeComponent();
			lsvLog.DoubleBuffered(true);

			activeMode = active;
			if (activeMode)
			{
				udpc = new UdpClient();
				udpc.Connect("localhost", 44558);
				udpc.Client.ReceiveTimeout = 5000;
			}

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
			setToolbarState();
			tabsViews.SelectedIndex = 1;
			log = newLog;
		}

		private void refresh(ROUND round = ROUND.ROUND_UP)
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
			log.Add(entry);
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
				case LogMsg.LOG_CONTEXT_SWITCH_ON:
				case LogMsg.LOG_CONTEXT_SWITCH_OFF:
				case LogMsg.LOG_TIMER:
				default:
					return false;
			}

			return true;
		}

		private int addToLog(LogEntry entry)
		{
			ListViewItem item = new ListViewItem();
			item.Text = entry.num.ToString();
			item.SubItems.Add(entry.msg.ToString());
			item.SubItems.Add(String.Join(" | ", entry.props));
			lsvLog.Items.Add(item);

			return item.Index;
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

		private static readonly byte[] pauseMsg  = new byte[] { (byte)DEBUG_COMMANDS.PAUSE };
		private static readonly byte[] resumeMsg = new byte[] { (byte)DEBUG_COMMANDS.CONTINUE };

		private void btnPause_Click(object sender, EventArgs e)
		{
			IPEndPoint ipep = null;
			byte[] ret;

			try
			{
				if (btnPause.Checked)
					udpc.Send(pauseMsg, pauseMsg.Length);
				else
					udpc.Send(resumeMsg, pauseMsg.Length);
				ret = udpc.Receive(ref ipep);
				if (ret[0] != 1)
					throw new Exception("Error processing request");
			}
			catch (Exception ex)
			{
				btnPause.Checked = !btnPause.Checked;
				MessageBox.Show(ex.Message);
				return;
			}

			setToolbarState();
		}

		private void setToolbarState()
		{
			btnSkipStart.Enabled =
				btnStepB.Enabled =
				btnStepF.Enabled =
				btnSkipEnd.Enabled = btnPause.Checked;
		}

		private void btnSkipStart_Click(object sender, EventArgs e)
		{
			displayUntil = 0;
			refresh();
		}

		private void btnSkipEnd_Click(object sender, EventArgs e)
		{
			displayUntil = log.Count;
			refresh();
		}

		private void btnStepB_Click(object sender, EventArgs e)
		{
			//if (displayUntil > 0)
				displayUntil--;
			refresh(ROUND.ROUND_DOWN);
		}

		private void btnStepF_Click(object sender, EventArgs e)
		{
			//if (displayUntil < log.Count)
				displayUntil++;
			refresh();
		}


		private enum ROUND
		{
			ROUND_UP,
			ROUND_DOWN,
			DONT_ROUND,
		}
	}
}
