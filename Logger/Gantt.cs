using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Logger
{
	public partial class Gantt : UserControl
	{
		private List<GanttSpan> spans = new List<GanttSpan>();
		private Dictionary<uint, GanttTask> tasks = new Dictionary<uint, GanttTask>();
		private uint sliceCount = 0;
		private Panel chart = new BufferedPanel() { Dock = DockStyle.Fill, AutoScroll = true };
		private int scale;
		private int rowHeight;

		public int RowHeight
		{
			get
			{
				return rowHeight;
			}
			set
			{
				rowHeight = value;
				foreach (Control label in splt.Panel1.Controls)
				{
					label.Height = rowHeight;
				}
				splt.Panel1.Controls[0].Height = (int)(rowHeight * 0.5);
				splt.Panel1.VerticalScroll.Maximum = (int)(rowHeight * (tasks.Count + 0.5));
				Invalidate();
			}
		}
		public int YScale
		{
			get
			{
				return scale;
			}
			set
			{
				scale = value;
				Invalidate();
			}
		}

		public void reset()
		{
			//spans.Clear();
			tasks.Clear();
			//Invalidate();
		}
		
		public void addTask(uint index, string name)
		{
			tasks.Add(index, new GanttTask(index, name, tasks.Count()));
			Label l = new Label()
				{
					Text = name,
					Height = RowHeight,
					Dock = DockStyle.Top,
					TextAlign = ContentAlignment.MiddleRight
				};
			splt.Panel1.Controls.Add(l);
			splt.Panel1.Controls.SetChildIndex(l, 1);
			splt.Panel1.VerticalScroll.Maximum = (int)(rowHeight * (tasks.Count + 0.5));
		}

		public void addSlice(uint index)
		{
			if (spans.Count > 0 && index == spans.Last().task.index)
			{
				spans.Last().duration++;
			}
			else
			{
				spans.Add(new GanttSpan() { task = tasks[index], start = sliceCount, duration = 1 });
			}
			sliceCount++;
			chart.Invalidate();
		}

		public Gantt()
		{
			InitializeComponent();
			splt.Panel1.Controls.Add(new Label() { Height = (int)(rowHeight * 0.5), Dock = DockStyle.Bottom });
			splt.Panel2.Controls.Add(chart);
			chart.Paint += Chart_Paint;
			chart.Scroll += Chart_Scroll;
			RowHeight = 50;
			YScale = 5;
		}

		private void Chart_Scroll(object sender, ScrollEventArgs e)
		{
			if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				splt.Panel1.VerticalScroll.Value = e.NewValue;
			}
		}

		private void Chart_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			bool autoScroll = (chart.Width - chart.AutoScrollPosition.X >= chart.AutoScrollMinSize.Width);
			chart.AutoScrollMinSize = new Size((int)sliceCount * scale, (int)(rowHeight * (tasks.Count + 0.5)));

			g.TranslateTransform(chart.AutoScrollPosition.X, chart.AutoScrollPosition.Y);
			foreach (var span in spans)
			{
				g.FillRectangle(span.task.brush, span.start * scale, RowHeight * span.task.order + RowHeight / 4, span.duration * scale, RowHeight / 2);
			}

			if (autoScroll)
				chart.HorizontalScroll.Value = chart.HorizontalScroll.Maximum;
		}

		private class GanttTask
		{
			private static readonly Random rand = new Random();
			public uint index { get; set; }
			public string name { get; set; }
			public Brush brush { get; set; }
			public int order { get; set; }
			public GanttTask(uint index, string name, int order)
			{
				this.index = index;
				this.name = name;
				this.order = order;
				brush = new SolidBrush(Color.FromArgb(rand.Next(192), rand.Next(192), rand.Next(192)));
			}
		}

		private class GanttSpan : IComparable<GanttSpan>
		{
			public GanttTask task { get; set; }
			public float start { get; set; }
			public float duration { get; set; }

			public int CompareTo(GanttSpan other)
			{
				return start.CompareTo(other.start);
			}
		}

		class BufferedPanel : Panel
		{
			public BufferedPanel()
			{
				this.DoubleBuffered = true;
			}
		}
	}
}
