namespace Logger
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tabViews = new System.Windows.Forms.TabControl();
            this.tabTaskList = new System.Windows.Forms.TabPage();
            this.lsvTasks = new System.Windows.Forms.ListView();
            this.tid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.state = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabGantt = new System.Windows.Forms.TabPage();
            this.ganttChart = new Logger.Gantt();
            this.Hide_tabs = new System.Windows.Forms.CheckBox();
            this.WindowSplitter = new System.Windows.Forms.SplitContainer();
            this.StopView = new System.Windows.Forms.CheckBox();
            this.tabViews.SuspendLayout();
            this.tabTaskList.SuspendLayout();
            this.tabGantt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WindowSplitter)).BeginInit();
            this.WindowSplitter.Panel1.SuspendLayout();
            this.WindowSplitter.Panel2.SuspendLayout();
            this.WindowSplitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabViews
            // 
            this.tabViews.Controls.Add(this.tabTaskList);
            this.tabViews.Controls.Add(this.tabGantt);
            this.tabViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabViews.Location = new System.Drawing.Point(0, 0);
            this.tabViews.Name = "tabViews";
            this.tabViews.SelectedIndex = 0;
            this.tabViews.Size = new System.Drawing.Size(614, 336);
            this.tabViews.TabIndex = 1;
            // 
            // tabTaskList
            // 
            this.tabTaskList.Controls.Add(this.lsvTasks);
            this.tabTaskList.Location = new System.Drawing.Point(4, 22);
            this.tabTaskList.Name = "tabTaskList";
            this.tabTaskList.Padding = new System.Windows.Forms.Padding(3);
            this.tabTaskList.Size = new System.Drawing.Size(606, 310);
            this.tabTaskList.TabIndex = 0;
            this.tabTaskList.Text = "Task Manager";
            this.tabTaskList.UseVisualStyleBackColor = true;
            // 
            // lsvTasks
            // 
            this.lsvTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tid,
            this.name,
            this.priority,
            this.state});
            this.lsvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvTasks.FullRowSelect = true;
            this.lsvTasks.Location = new System.Drawing.Point(3, 3);
            this.lsvTasks.MultiSelect = false;
            this.lsvTasks.Name = "lsvTasks";
            this.lsvTasks.Size = new System.Drawing.Size(600, 304);
            this.lsvTasks.TabIndex = 0;
            this.lsvTasks.UseCompatibleStateImageBehavior = false;
            this.lsvTasks.View = System.Windows.Forms.View.Details;
            // 
            // tid
            // 
            this.tid.Text = "ID";
            // 
            // name
            // 
            this.name.Text = "Name";
            // 
            // priority
            // 
            this.priority.Text = "Priority";
            // 
            // state
            // 
            this.state.Text = "State";
            // 
            // tabGantt
            // 
            this.tabGantt.Controls.Add(this.ganttChart);
            this.tabGantt.Location = new System.Drawing.Point(4, 22);
            this.tabGantt.Name = "tabGantt";
            this.tabGantt.Padding = new System.Windows.Forms.Padding(3);
            this.tabGantt.Size = new System.Drawing.Size(606, 310);
            this.tabGantt.TabIndex = 1;
            this.tabGantt.Text = "Gantt Chart";
            this.tabGantt.UseVisualStyleBackColor = true;
            // 
            // ganttChart
            // 
            this.ganttChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ganttChart.Location = new System.Drawing.Point(3, 3);
            this.ganttChart.Name = "ganttChart";
            this.ganttChart.RowHeight = 50;
            this.ganttChart.Size = new System.Drawing.Size(600, 304);
            this.ganttChart.TabIndex = 0;
            this.ganttChart.YScale = 5;
            // 
            // Hide_tabs
            // 
            this.Hide_tabs.Appearance = System.Windows.Forms.Appearance.Button;
            this.Hide_tabs.AutoSize = true;
            this.Hide_tabs.Location = new System.Drawing.Point(0, 0);
            this.Hide_tabs.Name = "Hide_tabs";
            this.Hide_tabs.Size = new System.Drawing.Size(62, 23);
            this.Hide_tabs.TabIndex = 2;
            this.Hide_tabs.Text = "Hide tabs";
            this.Hide_tabs.UseVisualStyleBackColor = true;
            this.Hide_tabs.CheckedChanged += new System.EventHandler(this.Show_tabs_CheckedChanged);
            // 
            // WindowSplitter
            // 
            this.WindowSplitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WindowSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WindowSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.WindowSplitter.Location = new System.Drawing.Point(0, 0);
            this.WindowSplitter.Name = "WindowSplitter";
            this.WindowSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // WindowSplitter.Panel1
            // 
            this.WindowSplitter.Panel1.Controls.Add(this.StopView);
            this.WindowSplitter.Panel1.Controls.Add(this.Hide_tabs);
            // 
            // WindowSplitter.Panel2
            // 
            this.WindowSplitter.Panel2.Controls.Add(this.tabViews);
            this.WindowSplitter.Size = new System.Drawing.Size(616, 367);
            this.WindowSplitter.SplitterDistance = 25;
            this.WindowSplitter.TabIndex = 3;
            // 
            // StopView
            // 
            this.StopView.Appearance = System.Windows.Forms.Appearance.Button;
            this.StopView.AutoSize = true;
            this.StopView.Location = new System.Drawing.Point(69, 0);
            this.StopView.Name = "StopView";
            this.StopView.Size = new System.Drawing.Size(64, 23);
            this.StopView.TabIndex = 3;
            this.StopView.Text = "Stop view";
            this.StopView.UseVisualStyleBackColor = true;
            this.StopView.CheckedChanged += new System.EventHandler(this.StopView_CheckedChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 367);
            this.Controls.Add(this.WindowSplitter);
            this.Name = "frmMain";
            this.Text = "Logger Test";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabViews.ResumeLayout(false);
            this.tabTaskList.ResumeLayout(false);
            this.tabGantt.ResumeLayout(false);
            this.WindowSplitter.Panel1.ResumeLayout(false);
            this.WindowSplitter.Panel1.PerformLayout();
            this.WindowSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WindowSplitter)).EndInit();
            this.WindowSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabViews;
		private System.Windows.Forms.TabPage tabTaskList;
		private System.Windows.Forms.TabPage tabGantt;
		private System.Windows.Forms.ListView lsvTasks;
		private System.Windows.Forms.ColumnHeader tid;
		private System.Windows.Forms.ColumnHeader name;
		private System.Windows.Forms.ColumnHeader priority;
		private System.Windows.Forms.ColumnHeader state;
		private Gantt ganttChart;
        private System.Windows.Forms.CheckBox Hide_tabs;
        private System.Windows.Forms.SplitContainer WindowSplitter;
        private System.Windows.Forms.SplitContainer TabSplitter;
        private System.Windows.Forms.CheckBox StopView;
	}
}

