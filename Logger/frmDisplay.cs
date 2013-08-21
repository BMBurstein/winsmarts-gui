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
		List<string[]> msgHistory = new List<string[]>();
		List<Task> tasks = new List<Task>();

		public frmDisplay()
		{
			InitializeComponent();
		}

		internal void done()
		{
			
		}

		internal void handleMsg(LogMsg msg, string s)
		{
			string[] parts = s.Split(';');

			msgHistory.Add(parts);
			switch (msg)
			{
				case LogMsg.LOG_NEW_TASK:
					handleDeclareTask(parts);
					break;
				case LogMsg.LOG_CONTEXT_SWITCH:
					handleContextSwitch(parts);
					break;
				case LogMsg.LOG_CONTEXT_SWITCH_ON:
					break;
				case LogMsg.LOG_CONTEXT_SWITCH_OFF:
					break;
				case LogMsg.LOG_TIMER:
					break;
				case LogMsg.LOG_TASK_STATUS_CHANGE:
					handleStatusChanged(parts);
					break;
				default:
					break;
			}
		}

		private void handleContextSwitch(string[] parts)
		{
			ganttChart.addSlice(uint.Parse(parts[0]));
		}

		private void handleDeclareTask(string[] parts)
		{
			Task task = new Task() { tid = uint.Parse(parts[0]), name = parts[1], priority = uint.Parse(parts[2]), state = TaskStates.READY };
			tasks.Add(task);
			
			ListViewItem item = new ListViewItem(parts.ToArray());
			if (item.SubItems[2].Text == uint.MaxValue.ToString())
				item.SubItems[2].Text = "MAX_INT";
			item.SubItems.Add("Ready");
			lsvTasks.Items.Add(item);

			ganttChart.addTask(task.tid, task.name);
		}

		private void handleStatusChanged(string[] parts)
		{
			foreach (ListViewItem item in lsvTasks.Items)
			{
				if (item.SubItems[0].Text == parts[0])
				{
					item.SubItems[3].Text = parts[1];
				}
			}
		}
	}
}
