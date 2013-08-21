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
		frmDisplay activeDisplay;
		List<frmDisplay> allDisplays = new List<frmDisplay>();

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

		// All log proccessing should be done here
		private void doStuff(string s)
		{
			LogMsg msg;

			if (s == "")
				msg = LogMsg.LOG_START;
			else
				msg = (LogMsg)s[4];

			switch (msg)
			{
				case LogMsg.LOG_START:
					if (activeDisplay != null)
					{
						activeDisplay.done();
						activeDisplay.Hide();
					}
					activeDisplay = new frmDisplay();
					allDisplays.Add(activeDisplay);
					activeDisplay.Show();
					break;
				default:
					if (activeDisplay != null)
						activeDisplay.handleMsg(msg, s.Substring(5));
					break;
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
