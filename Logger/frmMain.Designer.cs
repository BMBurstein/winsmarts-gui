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
			this.lstLogs = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// lstLogs
			// 
			this.lstLogs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstLogs.FormattingEnabled = true;
			this.lstLogs.Location = new System.Drawing.Point(0, 0);
			this.lstLogs.Name = "lstLogs";
			this.lstLogs.Size = new System.Drawing.Size(616, 367);
			this.lstLogs.TabIndex = 0;
			this.lstLogs.DoubleClick += new System.EventHandler(this.lstLogs_DoubleClick);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(616, 367);
			this.Controls.Add(this.lstLogs);
			this.Name = "frmMain";
			this.Text = "Logger Test";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lstLogs;
	}
}

