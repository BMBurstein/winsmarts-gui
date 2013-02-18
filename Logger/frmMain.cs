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

namespace Logger
{
	public partial class frmMain : Form
	{
		UdpClient udpc = new UdpClient(44557);

		public frmMain()
		{
			InitializeComponent();
		}

		private void ReceiveCallback(IAsyncResult ar)
		{
			IPEndPoint ipep = null;
			var rcv = udpc.EndReceive(ar, ref ipep);
			this.Invoke((MethodInvoker)(delegate
			{
				doStuff(Encoding.ASCII.GetString(rcv));
			}));
			udpc.BeginReceive(new AsyncCallback(ReceiveCallback), null);
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			udpc.BeginReceive(new AsyncCallback(ReceiveCallback), null);
		}

		// All log proccessing should be done here
		private void doStuff(string s)
		{
			string[] parts = s.Split(';');

			switch (parts[0])
			{
				case "DT":
					handleDeclareTask(parts);
					break;
				case "CS":
					handleContextSwitch(parts);
					break;
				default:
					break;
			}
		}

		List<string[]> CSHistory = new List<string[]>();
		private void handleContextSwitch(string[] parts)
		{
			ganttChart.addSlice(uint.Parse(parts[2]));
		}

		List<Task> tasks = new List<Task>();
		private void handleDeclareTask(string[] parts)
		{
			Task task = new Task() { tid = uint.Parse(parts[2]), name = parts[3], priority = uint.Parse(parts[4]), state = TaskStates.READY };
			tasks.Add(task);
			ListViewItem item = new ListViewItem(parts.Skip(2).ToArray());
			item.SubItems.Add("Ready");
			lsvTasks.Items.Add(item);
			ganttChart.addTask(task.tid, task.name);
		}
	}

	public enum TaskStates
	{
		READY,
		NOT_ACTIVE,
		SUSPENDED,
		SLEEPING,
		UNKNOWN
	}

	public class Task
	{
		public uint priority { get; set; }
		public string name { get; set; }
		public uint tid { get; set; }
		public TaskStates state { get; set; }
	}
}
