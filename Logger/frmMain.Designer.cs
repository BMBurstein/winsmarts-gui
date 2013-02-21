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
            this.Show_tabs = new System.Windows.Forms.CheckBox();
            this.ganttChart = new Logger.Gantt();
            this.tabViews.SuspendLayout();
            this.tabTaskList.SuspendLayout();
            this.tabGantt.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabViews
            // 
            this.tabViews.Controls.Add(this.tabTaskList);
            this.tabViews.Controls.Add(this.tabGantt);
            this.tabViews.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabViews.Location = new System.Drawing.Point(0, 26);
            this.tabViews.Name = "tabViews";
            this.tabViews.SelectedIndex = 0;
            this.tabViews.Size = new System.Drawing.Size(616, 341);
            this.tabViews.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabTaskList.Controls.Add(this.lsvTasks);
            this.tabTaskList.Location = new System.Drawing.Point(4, 22);
            this.tabTaskList.Name = "tabPage1";
            this.tabTaskList.Padding = new System.Windows.Forms.Padding(3);
            this.tabTaskList.Size = new System.Drawing.Size(608, 315);
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
            this.lsvTasks.Size = new System.Drawing.Size(602, 309);
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
            // tabPage2
            // 
            this.tabGantt.Controls.Add(this.ganttChart);
            this.tabGantt.Location = new System.Drawing.Point(4, 22);
            this.tabGantt.Name = "tabPage2";
            this.tabGantt.Padding = new System.Windows.Forms.Padding(3);
            this.tabGantt.Size = new System.Drawing.Size(608, 315);
            this.tabGantt.TabIndex = 1;
            this.tabGantt.Text = "Gantt Chart";
            this.tabGantt.UseVisualStyleBackColor = true;
            // 
            // Show_tabs
            // 
            this.Show_tabs.AutoSize = true;
            this.Show_tabs.Checked = true;
            this.Show_tabs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Show_tabs.Location = new System.Drawing.Point(0, 3);
            this.Show_tabs.Name = "Show_tabs";
            this.Show_tabs.Size = new System.Drawing.Size(76, 17);
            this.Show_tabs.TabIndex = 2;
            this.Show_tabs.Text = "Show tabs";
            this.Show_tabs.UseVisualStyleBackColor = true;
            this.Show_tabs.CheckedChanged += new System.EventHandler(this.Show_tabs_CheckedChanged);
            // 
            // ganttChart
            // 
            this.ganttChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ganttChart.Location = new System.Drawing.Point(3, 3);
            this.ganttChart.Name = "ganttChart";
            this.ganttChart.Size = new System.Drawing.Size(602, 309);
            this.ganttChart.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 367);
            this.Controls.Add(this.Show_tabs);
            this.Controls.Add(this.tabViews);
            this.Name = "frmMain";
            this.Text = "Logger Test";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabViews.ResumeLayout(false);
            this.tabTaskList.ResumeLayout(false);
            this.tabGantt.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.CheckBox Show_tabs;
	}
}

