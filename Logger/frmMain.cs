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
            if (this.StopView.Checked)
            {
                //ganttChart.StopView = false;
            }
            else
            {
                //ganttChart.StopView = true;
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
