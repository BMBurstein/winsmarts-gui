namespace Logger
{
	partial class Gantt
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splt = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.splt)).BeginInit();
			this.splt.SuspendLayout();
			this.SuspendLayout();
			// 
			// splt
			// 
			this.splt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splt.Location = new System.Drawing.Point(0, 0);
			this.splt.Name = "splt";
			// 
			// splt.Panel2
			// 
			this.splt.Panel2.AutoScroll = true;
			this.splt.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
			this.splt.Size = new System.Drawing.Size(288, 150);
			this.splt.SplitterDistance = 96;
			this.splt.TabIndex = 0;
			// 
			// Gantt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splt);
			this.Name = "Gantt";
			this.Size = new System.Drawing.Size(288, 150);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Gantt_Paint);
			((System.ComponentModel.ISupportInitialize)(this.splt)).EndInit();
			this.splt.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splt;
	}
}
