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
			this.Invoke((Action)(()=>doStuff(Encoding.ASCII.GetString(rcv))));
			udpc.BeginReceive(new AsyncCallback(ReceiveCallback), null);
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			udpc.BeginReceive(new AsyncCallback(ReceiveCallback), null);
		}

		// All log proccessing should be done here
		private void doStuff(string s)
		{
			lsbLog.Items.Add(s);
		}
	}
}
