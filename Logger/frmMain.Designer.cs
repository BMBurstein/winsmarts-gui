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
			this.WindowSplitter = new System.Windows.Forms.SplitContainer();
			this.lstLogs = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.WindowSplitter)).BeginInit();
			this.WindowSplitter.Panel1.SuspendLayout();
			this.WindowSplitter.SuspendLayout();
			this.SuspendLayout();
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
			this.WindowSplitter.Panel1.Controls.Add(this.lstLogs);
			this.WindowSplitter.Size = new System.Drawing.Size(616, 367);
			this.WindowSplitter.SplitterDistance = 219;
			this.WindowSplitter.TabIndex = 3;
			// 
			// lstLogs
			// 
			this.lstLogs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstLogs.FormattingEnabled = true;
			this.lstLogs.Location = new System.Drawing.Point(0, 0);
			this.lstLogs.Name = "lstLogs";
			this.lstLogs.Size = new System.Drawing.Size(614, 217);
			this.lstLogs.TabIndex = 0;
			this.lstLogs.DoubleClick += new System.EventHandler(this.lstLogs_DoubleClick);
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
			this.WindowSplitter.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.WindowSplitter)).EndInit();
			this.WindowSplitter.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer WindowSplitter;
		private System.Windows.Forms.SplitContainer TabSplitter;
		private System.Windows.Forms.ListBox lstLogs;
	}
}

