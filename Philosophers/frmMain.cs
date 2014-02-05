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

namespace Philosophers
{
	public partial class frmMain : Form
	{
		UdpClient udpc = new UdpClient(44557);
		List<Philosopher> phils;
		Dictionary<PhilStates, Brush> b = new Dictionary<PhilStates, Brush>();

		public frmMain()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			b[PhilStates.EATING] = Brushes.Green;
			b[PhilStates.THINKING] = Brushes.Gray;
			b[PhilStates.WAITING] = Brushes.Yellow;

			udpc.BeginReceive(new AsyncCallback(ReceiveCallback), null);
		}

		private void ReceiveCallback(IAsyncResult ar)
		{
			IPEndPoint ipep = null;
			var rcv = udpc.EndReceive(ar, ref ipep);
			try
			{
				this.Invoke((MethodInvoker)(delegate
					{
						updateDisplay(rcv);
					}));
				udpc.BeginReceive(new AsyncCallback(ReceiveCallback), null);
			}
			catch (Exception) { }
		}

		private void updateDisplay(byte[] s)
		{
			LogEntry entry = new LogEntry();

			entry.msg = (LogMsg)s[0];

			if (entry.msg == LogMsg.LOG_START)
				phils = new List<Philosopher>();
			else
			{
				if (phils == null)
					return;
				Philosopher p;
				entry.props = Encoding.ASCII.GetString(s, 5, s.Length - 5).Split(';');
				switch (entry.msg)
				{
					case LogMsg.LOG_NEW_TASK:
						p = new Philosopher();
						p.tid = uint.Parse(entry.props[0]);
						if (p.tid == 0)
							return;
						p.name = entry.props[1];
						phils.Add(p);
						break;
					case LogMsg.LOG_TASK_STATUS_CHANGE:
						TaskStates state = (TaskStates)int.Parse(entry.props[1]);
						uint tid = uint.Parse(entry.props[0]);
						if (tid == 0)
							return;
						p = (from temp in phils where temp.tid == tid select temp).First();
						switch (state)
						{
							case TaskStates.READY:
								p.state = PhilStates.EATING;
								break;
							case TaskStates.SUSPENDED:
								p.state = PhilStates.WAITING;
								break;
							case TaskStates.SLEEPING:
								p.state = PhilStates.THINKING;
								break;
							case TaskStates.RUNNING:
								p.state = PhilStates.EATING;
								break;
						}
						break;
				}
				Refresh();
			}
		}

		private enum PhilStates
		{
			EATING,
			THINKING,
			WAITING,
		}

		class Philosopher
		{
			public PhilStates state;
			public string name;
			public uint tid;
		}

		private void frmMain_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			g.Clear(Color.White);

			if (phils == null)
				return;

			for (int i = 0; i < phils.Count; i++)
			{
				g.FillEllipse(b[phils[i].state], (float)(100f * Math.Cos(Math.PI / (float)phils.Count * (float)i * 2f)) + Width / 2, (float)(100f * Math.Sin(Math.PI / (float)phils.Count * (float)i * 2f)) + Height / 2, 60f, 60f);
			}
		}
	}
}