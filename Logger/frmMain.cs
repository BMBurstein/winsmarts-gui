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
	public partial class frmMain : Form
	{
		UdpClient udpc = new UdpClient(44557);
		List<ObservableCollection<LogEntry>> allLogs = new List<ObservableCollection<LogEntry>>();

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
				doStuff(rcv);
			}));
			udpc.BeginReceive(new AsyncCallback(ReceiveCallback), null);
		}

		private void doStuff(byte[] s)
		{
			LogEntry entry = new LogEntry();

			entry.msg = (LogMsg)s[0];

			switch (entry.msg)
			{
				case LogMsg.LOG_START:
					if (frmDisplay.activeDisplay == null || frmDisplay.activeDisplay.IsDisposed)
					{
						frmDisplay.activeDisplay = new frmDisplay();
					}
					else
					{
						frmDisplay.activeDisplay.Clear();
					}
					allLogs.Add(frmDisplay.activeDisplay.log);
					lstLogs.Items.Add("Log #" + allLogs.Count);
					frmDisplay.activeDisplay.Text = "Log #" + allLogs.Count + " (active)";
					frmDisplay.activeDisplay.Show();
					frmDisplay.activeDisplay.BringToFront();
					WindowState = FormWindowState.Minimized;
					goto default; //fallthrough
				default:
					if (frmDisplay.activeDisplay != null)
					{
						entry.num = (uint)IPAddress.NetworkToHostOrder(BitConverter.ToInt32(s, 1));
						entry.props = Encoding.ASCII.GetString(s, 5, s.Length - 5).Split(';');
						frmDisplay.activeDisplay.log.Add(entry);
					}
					break;
			}
		}

		private void lstLogs_DoubleClick(object sender, EventArgs e)
		{
			if (lstLogs.SelectedIndex == lstLogs.Items.Count - 1)
			{
				if (frmDisplay.activeDisplay.IsDisposed)
				{
					frmDisplay.activeDisplay = new frmDisplay(allLogs.Last());
					frmDisplay.activeDisplay.Text = "Log #" + allLogs.Count + " (active)";
				}
				frmDisplay.activeDisplay.Show();
				if (frmDisplay.activeDisplay.WindowState == FormWindowState.Minimized)
					frmDisplay.activeDisplay.WindowState = FormWindowState.Normal;
				frmDisplay.activeDisplay.BringToFront();
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
