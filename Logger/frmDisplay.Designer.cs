﻿namespace Logger
{
	partial class frmDisplay
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisplay));
			this.tabsViews = new System.Windows.Forms.TabControl();
			this.tabTaskList = new System.Windows.Forms.TabPage();
			this.lsvTasks = new System.Windows.Forms.ListView();
			this.tid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.state = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabGantt = new System.Windows.Forms.TabPage();
			this.ganttChart = new Logger.Gantt();
			this.tabLocks = new System.Windows.Forms.TabPage();
			this.lsvLocks = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabLog = new System.Windows.Forms.TabPage();
			this.lsvLog = new System.Windows.Forms.ListView();
			this.colOrder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnPause = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnSkipStart = new System.Windows.Forms.ToolStripButton();
			this.btnStepB = new System.Windows.Forms.ToolStripButton();
			this.btnStepF = new System.Windows.Forms.ToolStripButton();
			this.btnSkipEnd = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnStepRun = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.btnSetTask = new System.Windows.Forms.ToolStripDropDownButton();
			this.tabsViews.SuspendLayout();
			this.tabTaskList.SuspendLayout();
			this.tabGantt.SuspendLayout();
			this.tabLocks.SuspendLayout();
			this.tabLog.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabsViews
			// 
			this.tabsViews.Controls.Add(this.tabTaskList);
			this.tabsViews.Controls.Add(this.tabGantt);
			this.tabsViews.Controls.Add(this.tabLocks);
			this.tabsViews.Controls.Add(this.tabLog);
			this.tabsViews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabsViews.Location = new System.Drawing.Point(0, 25);
			this.tabsViews.Name = "tabsViews";
			this.tabsViews.SelectedIndex = 0;
			this.tabsViews.Size = new System.Drawing.Size(715, 369);
			this.tabsViews.TabIndex = 2;
			this.tabsViews.Resize += new System.EventHandler(this.tabsViews_Resize);
			// 
			// tabTaskList
			// 
			this.tabTaskList.Controls.Add(this.lsvTasks);
			this.tabTaskList.Location = new System.Drawing.Point(4, 22);
			this.tabTaskList.Name = "tabTaskList";
			this.tabTaskList.Padding = new System.Windows.Forms.Padding(3);
			this.tabTaskList.Size = new System.Drawing.Size(707, 343);
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
			this.lsvTasks.Size = new System.Drawing.Size(701, 337);
			this.lsvTasks.TabIndex = 0;
			this.lsvTasks.UseCompatibleStateImageBehavior = false;
			this.lsvTasks.View = System.Windows.Forms.View.Details;
			// 
			// tid
			// 
			this.tid.Text = "ID";
			this.tid.Width = 30;
			// 
			// name
			// 
			this.name.Text = "Name";
			this.name.Width = 136;
			// 
			// priority
			// 
			this.priority.Text = "Priority";
			// 
			// state
			// 
			this.state.Text = "State";
			this.state.Width = 239;
			// 
			// tabGantt
			// 
			this.tabGantt.Controls.Add(this.ganttChart);
			this.tabGantt.Location = new System.Drawing.Point(4, 22);
			this.tabGantt.Name = "tabGantt";
			this.tabGantt.Padding = new System.Windows.Forms.Padding(3);
			this.tabGantt.Size = new System.Drawing.Size(707, 343);
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
			this.ganttChart.Size = new System.Drawing.Size(701, 337);
			this.ganttChart.TabIndex = 0;
			this.ganttChart.YScale = 5;
			// 
			// tabLocks
			// 
			this.tabLocks.Controls.Add(this.lsvLocks);
			this.tabLocks.Location = new System.Drawing.Point(4, 22);
			this.tabLocks.Name = "tabLocks";
			this.tabLocks.Padding = new System.Windows.Forms.Padding(3);
			this.tabLocks.Size = new System.Drawing.Size(707, 343);
			this.tabLocks.TabIndex = 3;
			this.tabLocks.Text = "Locks";
			this.tabLocks.UseVisualStyleBackColor = true;
			// 
			// lsvLocks
			// 
			this.lsvLocks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.lsvLocks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvLocks.Location = new System.Drawing.Point(3, 3);
			this.lsvLocks.Name = "lsvLocks";
			this.lsvLocks.Size = new System.Drawing.Size(701, 337);
			this.lsvLocks.TabIndex = 0;
			this.lsvLocks.UseCompatibleStateImageBehavior = false;
			this.lsvLocks.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Lock";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Count";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Holding";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Waiting";
			// 
			// tabLog
			// 
			this.tabLog.Controls.Add(this.lsvLog);
			this.tabLog.Location = new System.Drawing.Point(4, 22);
			this.tabLog.Name = "tabLog";
			this.tabLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabLog.Size = new System.Drawing.Size(707, 343);
			this.tabLog.TabIndex = 2;
			this.tabLog.Text = "Full log";
			this.tabLog.UseVisualStyleBackColor = true;
			// 
			// lsvLog
			// 
			this.lsvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOrder,
            this.colAction,
            this.colData});
			this.lsvLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvLog.FullRowSelect = true;
			this.lsvLog.HideSelection = false;
			this.lsvLog.Location = new System.Drawing.Point(3, 3);
			this.lsvLog.MultiSelect = false;
			this.lsvLog.Name = "lsvLog";
			this.lsvLog.Size = new System.Drawing.Size(701, 337);
			this.lsvLog.TabIndex = 0;
			this.lsvLog.UseCompatibleStateImageBehavior = false;
			this.lsvLog.View = System.Windows.Forms.View.Details;
			this.lsvLog.ItemActivate += new System.EventHandler(this.lsvLog_ItemActivate);
			// 
			// colOrder
			// 
			this.colOrder.Text = "Order";
			// 
			// colAction
			// 
			this.colAction.Text = "Action";
			this.colAction.Width = 159;
			// 
			// colData
			// 
			this.colData.Text = "Data";
			this.colData.Width = 317;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPause,
            this.toolStripSeparator1,
            this.btnSkipStart,
            this.btnStepB,
            this.btnStepF,
            this.btnSkipEnd,
            this.toolStripSeparator2,
            this.btnStepRun,
            this.toolStripSeparator3,
            this.btnSetTask});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(715, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnPause
			// 
			this.btnPause.CheckOnClick = true;
			this.btnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
			this.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(23, 22);
			this.btnPause.Text = "ll";
			this.btnPause.ToolTipText = "Pause";
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// btnSkipStart
			// 
			this.btnSkipStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnSkipStart.Image = ((System.Drawing.Image)(resources.GetObject("btnSkipStart.Image")));
			this.btnSkipStart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSkipStart.Name = "btnSkipStart";
			this.btnSkipStart.Size = new System.Drawing.Size(27, 22);
			this.btnSkipStart.Text = "<<";
			this.btnSkipStart.ToolTipText = "Skip to start";
			this.btnSkipStart.Click += new System.EventHandler(this.btnSkipStart_Click);
			// 
			// btnStepB
			// 
			this.btnStepB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnStepB.Image = ((System.Drawing.Image)(resources.GetObject("btnStepB.Image")));
			this.btnStepB.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStepB.Name = "btnStepB";
			this.btnStepB.Size = new System.Drawing.Size(23, 22);
			this.btnStepB.Text = "<";
			this.btnStepB.ToolTipText = "Step Back";
			this.btnStepB.Click += new System.EventHandler(this.btnStepB_Click);
			// 
			// btnStepF
			// 
			this.btnStepF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnStepF.Image = ((System.Drawing.Image)(resources.GetObject("btnStepF.Image")));
			this.btnStepF.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStepF.Name = "btnStepF";
			this.btnStepF.Size = new System.Drawing.Size(23, 22);
			this.btnStepF.Text = ">";
			this.btnStepF.ToolTipText = "Step foreward";
			this.btnStepF.Click += new System.EventHandler(this.btnStepF_Click);
			// 
			// btnSkipEnd
			// 
			this.btnSkipEnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnSkipEnd.Image = ((System.Drawing.Image)(resources.GetObject("btnSkipEnd.Image")));
			this.btnSkipEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSkipEnd.Name = "btnSkipEnd";
			this.btnSkipEnd.Size = new System.Drawing.Size(27, 22);
			this.btnSkipEnd.Text = ">>";
			this.btnSkipEnd.ToolTipText = "Skip to end";
			this.btnSkipEnd.Click += new System.EventHandler(this.btnSkipEnd_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// btnStepRun
			// 
			this.btnStepRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnStepRun.Image = ((System.Drawing.Image)(resources.GetObject("btnStepRun.Image")));
			this.btnStepRun.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnStepRun.Name = "btnStepRun";
			this.btnStepRun.Size = new System.Drawing.Size(23, 22);
			this.btnStepRun.Text = "|>";
			this.btnStepRun.ToolTipText = "Step run";
			this.btnStepRun.Click += new System.EventHandler(this.btnStepRun_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// btnSetTask
			// 
			this.btnSetTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnSetTask.Image = ((System.Drawing.Image)(resources.GetObject("btnSetTask.Image")));
			this.btnSetTask.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSetTask.Name = "btnSetTask";
			this.btnSetTask.Size = new System.Drawing.Size(44, 22);
			this.btnSetTask.Text = "Task";
			this.btnSetTask.ToolTipText = "Set Task";
			this.btnSetTask.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.btnSetTask_DropDownItemClicked);
			// 
			// frmDisplay
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(715, 394);
			this.Controls.Add(this.tabsViews);
			this.Controls.Add(this.toolStrip1);
			this.Name = "frmDisplay";
			this.Text = "frmDisplay";
			this.tabsViews.ResumeLayout(false);
			this.tabTaskList.ResumeLayout(false);
			this.tabGantt.ResumeLayout(false);
			this.tabLocks.ResumeLayout(false);
			this.tabLog.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabsViews;
		private System.Windows.Forms.TabPage tabTaskList;
		private System.Windows.Forms.ListView lsvTasks;
		private System.Windows.Forms.ColumnHeader tid;
		private System.Windows.Forms.ColumnHeader name;
		private System.Windows.Forms.ColumnHeader priority;
		private System.Windows.Forms.ColumnHeader state;
		private System.Windows.Forms.TabPage tabGantt;
		private Gantt ganttChart;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnPause;
		private System.Windows.Forms.ToolStripButton btnStepF;
		private System.Windows.Forms.ToolStripButton btnStepB;
		private System.Windows.Forms.ToolStripButton btnSkipStart;
		private System.Windows.Forms.ToolStripButton btnSkipEnd;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.TabPage tabLog;
		private System.Windows.Forms.ListView lsvLog;
		private System.Windows.Forms.ColumnHeader colOrder;
		private System.Windows.Forms.ColumnHeader colAction;
		private System.Windows.Forms.ColumnHeader colData;
		private System.Windows.Forms.ToolStripButton btnStepRun;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripDropDownButton btnSetTask;
		private System.Windows.Forms.TabPage tabLocks;
		private System.Windows.Forms.ListView lsvLocks;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
	}
}