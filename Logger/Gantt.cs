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
		//private List<GanttSpan> spans = new List<GanttSpan>();
		private Dictionary<uint, GanttTask> tasks = new Dictionary<uint, GanttTask>();
		private uint sliceCount = 0;
		private Panel chart = new BufferedPanel() { Dock = DockStyle.Fill, AutoScroll = true };
		private int scale;
		private int rowHeight;

		public Dictionary<TaskStates, Brush> colors = new Dictionary<TaskStates, Brush>();

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

		public void Clear()
		{
			tasks.Clear();
			splt.Panel1.Controls.Clear();
			splt.Panel1.Controls.Add(new Label() { Height = (int)(rowHeight * 0.5), Dock = DockStyle.Bottom });
			sliceCount = 0;
			Invalidate();
		}
		
		public void addTask(uint index, string name)
		{
			tasks.Add(index, new GanttTask(index, name, tasks.Count()));
			Label l = new Label()
				{
					Text = name,
					Height = RowHeight,
					Dock = DockStyle.Top, //todo: maybe dock to bottom and no need to play with ChildIndex?
					TextAlign = ContentAlignment.MiddleRight
				};
			splt.Panel1.Controls.Add(l);
			splt.Panel1.Controls.SetChildIndex(l, 1);
			splt.Panel1.VerticalScroll.Maximum = (int)(rowHeight * (tasks.Count + 0.5));
			Invalidate();
		}

		public void changeState(uint index, TaskStates state, bool nextTick)
		{
			if (tasks[index].spans.Count == 0 || state != (tasks[index].spans.Last().type))
			{
				tasks[index].spans.Add(new GanttSpan() { type = state, start = sliceCount });
			}
			if(nextTick)
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

			colors[TaskStates.NOT_ACTIVE] = Brushes.Transparent;
			colors[TaskStates.READY] = Brushes.LightGray;
			colors[TaskStates.RUNNING] = Brushes.LightGreen;
			colors[TaskStates.SLEEPING] = Brushes.Navy;
			colors[TaskStates.SUSPENDED] = Brushes.Gray;
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

			foreach (var task in tasks.Values)
			{
				for (int i=0; i<task.spans.Count-1; i++)
				{
					GanttSpan span = task.spans[i];
					g.FillRectangle(colors[span.type] ?? Brushes.Transparent, span.start * scale, RowHeight * task.order + RowHeight / 4, (task.spans[i + 1].start - span.start) * scale, RowHeight / 2);
				}
				g.FillRectangle(colors[task.spans.Last().type] ?? Brushes.Transparent, task.spans.Last().start * scale, RowHeight * task.order + RowHeight / 4, (sliceCount - task.spans.Last().start) * scale, RowHeight / 2);
			}

			if (autoScroll)
				chart.HorizontalScroll.Value = chart.HorizontalScroll.Maximum;
		}

		private class GanttTask
		{
			public uint index { get; set; }
			public string name { get; set; }
			public int order { get; set; }
			public List<GanttSpan> spans = new List<GanttSpan>();
			public GanttTask(uint index, string name, int order)
			{
				this.index = index;
				this.name = name;
				this.order = order;
				this.spans.Add(new GanttSpan() { type = TaskStates.READY, start = 0 });
			}
		}

		private class GanttSpan
		{
			public TaskStates type { get; set; }
			public float start { get; set; }
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
