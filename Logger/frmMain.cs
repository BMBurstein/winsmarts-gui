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
	}
}
