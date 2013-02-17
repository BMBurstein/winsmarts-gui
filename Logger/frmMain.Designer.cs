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
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lsvTasks = new System.Windows.Forms.ListView();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.priority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabViews.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabViews
			// 
			this.tabViews.Controls.Add(this.tabPage1);
			this.tabViews.Controls.Add(this.tabPage2);
			this.tabViews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabViews.Location = new System.Drawing.Point(0, 0);
			this.tabViews.Name = "tabViews";
			this.tabViews.SelectedIndex = 0;
			this.tabViews.Size = new System.Drawing.Size(284, 262);
			this.tabViews.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lsvTasks);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(276, 236);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Task Manager";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// lsvTasks
			// 
			this.lsvTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tid,
            this.name,
            this.priority});
			this.lsvTasks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvTasks.FullRowSelect = true;
			this.lsvTasks.Location = new System.Drawing.Point(3, 3);
			this.lsvTasks.MultiSelect = false;
			this.lsvTasks.Name = "lsvTasks";
			this.lsvTasks.Size = new System.Drawing.Size(270, 230);
			this.lsvTasks.TabIndex = 0;
			this.lsvTasks.UseCompatibleStateImageBehavior = false;
			this.lsvTasks.View = System.Windows.Forms.View.Details;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(276, 236);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
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
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.tabViews);
			this.Name = "frmMain";
			this.Text = "Logger Test";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.tabViews.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabViews;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView lsvTasks;
		private System.Windows.Forms.ColumnHeader tid;
		private System.Windows.Forms.ColumnHeader name;
		private System.Windows.Forms.ColumnHeader priority;
	}
}

