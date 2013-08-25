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
		frmDisplay activeDisplay = new frmDisplay();
		List<List<LogEntry>> allLogs = new List<List<LogEntry>>();

		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			udpc.BeginReceive(new AsyncCallback(ReceiveCallback), null);
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

		private void doStuff(string s)
		{
			LogEntry entry = new LogEntry();

			entry.msg = (LogMsg)s[0];

			switch (entry.msg)
			{
				case LogMsg.LOG_START:
					if (activeDisplay.IsDisposed)
					{
						activeDisplay = new frmDisplay();
					}
					else
					{
						activeDisplay.Clear();
					}
					activeDisplay.Show();
					activeDisplay.BringToFront();
					allLogs.Add(activeDisplay.log);
					lstLogs.Items.Add("Log #" + allLogs.Count);
					activeDisplay.Text = "Log #" + allLogs.Count + " (active)";
					break;
				default:
					entry.num = BitConverter.ToUInt32(s.ToByteArray(), 1);
					entry.props = s.Substring(5).Split(';');
					activeDisplay.handleMsg(entry);
					break;
			}
		}

		private void lstLogs_DoubleClick(object sender, EventArgs e)
		{
			if (lstLogs.SelectedIndex == lstLogs.Items.Count - 1)
			{
				if (activeDisplay.IsDisposed)
				{
					activeDisplay = new frmDisplay(allLogs.Last());
					activeDisplay.Text = "Log #" + allLogs.Count + " (active)";
				}
				activeDisplay.Show();
				if (activeDisplay.WindowState == FormWindowState.Minimized)
					activeDisplay.WindowState = FormWindowState.Normal;
				activeDisplay.BringToFront();
			}
			else
			{
				var disp = new frmDisplay(allLogs[lstLogs.SelectedIndex], false);
				disp.Text = lstLogs.SelectedItem.ToString();
				disp.Show();
				disp.BringToFront();
			}
		}

		
		/*
		private void Show_tabs_CheckedChanged(object sender, EventArgs e)
		{
			if (Hide_tabs.Checked)
			{
				if (this.TabSplitter == null)
					CreateTabSplitter();

				this.tabViews.Visible = false;
				this.TabSplitter.Visible = true;
				this.WindowSplitter.Panel2.Controls.Add(this.TabSplitter);
				this.TabSplitter.SplitterDistance = WindowSplitter.Width / 2;
				this.TabSplitter.Panel1.Controls.Add(this.lsvTasks);
				this.TabSplitter.Panel2.Controls.Add(this.ganttChart);
			}
			else
			{
				this.TabSplitter.Visible = false;
				this.tabViews.Visible = true;
				this.WindowSplitter.Panel2.Controls.Add(this.tabViews);
				this.tabTaskList.Controls.Add(this.lsvTasks);
				this.tabGantt.Controls.Add(this.ganttChart);
			}
		}

		private void CreateTabSplitter()
		{
			this.TabSplitter = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.TabSplitter)).BeginInit();
			this.TabSplitter.SuspendLayout();
			this.TabSplitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TabSplitter.Location = new System.Drawing.Point(370, 70);
			this.TabSplitter.Name = "TabSplitter";
			this.TabSplitter.Size = new System.Drawing.Size(150, 100);
			this.TabSplitter.SplitterDistance = 121;
			this.TabSplitter.TabIndex = 2;
			this.TabSplitter.Visible = false;
			this.TabSplitter.Dock = DockStyle.Fill;
			((System.ComponentModel.ISupportInitialize)(this.TabSplitter)).EndInit();
			this.TabSplitter.ResumeLayout(false);
		}

		private void StopView_CheckedChanged(object sender, EventArgs e)
		{
			if (!StopView.Checked)
				udpc.BeginReceive(new AsyncCallback(ReceiveCallback), null);
		}*/
	}
}
