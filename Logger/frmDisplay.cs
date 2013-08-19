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

		internal void handleMsg(string[] parts)
		{
			msgHistory.Add(parts);
			switch (parts[0])
			{
				case "DeclareTask":
					handleDeclareTask(parts);
					break;
				case "ContextSwitch":
					handleContextSwitch(parts);
					break;
				case "StatusChanged":
					handleStatusChanged(parts);
					break;

				default:
					break;
			}
		}

		private void handleContextSwitch(string[] parts)
		{
			ganttChart.addSlice(uint.Parse(parts[2]));
		}

		private void handleDeclareTask(string[] parts)
		{
			Task task = new Task() { tid = uint.Parse(parts[2]), name = parts[3], priority = uint.Parse(parts[4]), state = TaskStates.READY };
			tasks.Add(task);
			
			ListViewItem item = new ListViewItem(parts.Skip(2).ToArray());
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
				if (item.SubItems[0].Text == parts[2])
				{
					item.SubItems[3].Text = parts[3];
				}
			}
		}
	}
}
