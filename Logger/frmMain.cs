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
        int cycles = -1;

        public frmMain()
        {
            InitializeComponent();
            this.tabViews.Dock = DockStyle.Bottom;
            this.tabViews.Size = new System.Drawing.Size(this.tabViews.Size.Width, this.Size.Height - 60);
        }

        private void reset()
        {
            tasks.Clear();
            //lsvTasks.Items.Clear();
            ganttChart.reset();
            cycles++;
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
                case "Start":
                    reset();
                    break;
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
            if (item.SubItems[2].Text == uint.MaxValue.ToString())
                item.SubItems[2].Text = "MAX_INT";
            item.SubItems.Add("Ready");
            lsvTasks.Items.Add(item);
            ganttChart.addTask(task.tid, task.name);
        }

        private void handleStatusChanged(string[] parts)
        {
            int jump = 0;
            foreach (ListViewItem item in lsvTasks.Items)
            {
                if (item.SubItems[0].Text == parts[2])
                {
                    if (cycles == jump++)
                    {
                        item.SubItems[3].Text = parts[3];
                        break;
                    }
                }
            }
        }

        private void Show_tabs_CheckedChanged(object sender, EventArgs e)
        {
            if (Show_tabs.Checked)
            {
                this.tabPage1.Controls.Add(this.lsvTasks);
                this.lsvTasks.Location = new Point(this.lsvTasks.Location.X, this.lsvTasks.Location.Y - 20);
                this.lsvTasks.Dock = DockStyle.Fill;

                this.tabPage2.Controls.Add(this.ganttChart);
                this.ganttChart.Location = new Point(this.Size.Width / 2, this.ganttChart.Location.Y - 20);
                this.ganttChart.Dock = DockStyle.Fill;

                tabViews.Visible = true;
            }
            else
            {
                this.tabViews.Size = new System.Drawing.Size(this.tabViews.Size.Width, this.Size.Height - 60);

                this.Controls.Add(this.lsvTasks);
                this.lsvTasks.Dock = DockStyle.None;
                this.lsvTasks.Size = new System.Drawing.Size(this.Size.Width / 2 - 10, this.Size.Height - 60);
                this.lsvTasks.Location = new Point(this.lsvTasks.Location.X, this.lsvTasks.Location.Y + 20);

                this.Controls.Add(this.ganttChart);
                this.ganttChart.Dock = DockStyle.None;
                this.ganttChart.Size = new System.Drawing.Size(this.Size.Width / 2 - 10, this.Size.Height - 60);
                this.ganttChart.Location = new Point(this.Size.Width / 2, this.ganttChart.Location.Y + 20);

                tabViews.Visible = false;
            }
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
