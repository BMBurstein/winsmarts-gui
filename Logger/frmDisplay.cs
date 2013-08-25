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

		private List<LogEntry> log_;
		public List<LogEntry> log
		{
			get { return log_; }
			set
			{
				if (value == null)
					log_ = new List<LogEntry>();
				else
					log_ = value;
				reset();
			}
		}

		public frmDisplay()
		{
			InitializeComponent();
			Clear();
		}

		public void Clear()
		{
			log = null;
		}

		internal void reset()
		{
			ganttChart.Clear();
			lsvTasks.Items.Clear();
			foreach (var entry in log)
			{
				handleMsg(entry, false);
			}
		}

		internal void handleMsg(LogEntry entry, bool buildLog = true)
		{
			if(buildLog)
				log.Add(entry);

			switch (entry.msg)
			{
				case LogMsg.LOG_NEW_TASK:
					handleDeclareTask(entry);
					break;
				case LogMsg.LOG_CONTEXT_SWITCH:
					handleContextSwitch(entry);
					break;
				case LogMsg.LOG_CONTEXT_SWITCH_ON:
					break;
				case LogMsg.LOG_CONTEXT_SWITCH_OFF:
					break;
				case LogMsg.LOG_TIMER:
					break;
				case LogMsg.LOG_TASK_STATUS_CHANGE:
					handleStatusChanged(entry);
					break;
				default:
					break;
			}
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
				item.SubItems[2].Text = "MAX_INT";
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
				}
			}
		}
	}
}
