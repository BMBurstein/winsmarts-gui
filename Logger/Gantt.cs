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
		//private List<string> names = new List<string>();
		private SortedList<uint, GanttTask> tasks = new SortedList<uint, GanttTask>();
		private uint sliceCount = 0;

		public void reset()
		{
			//spans.Clear();
			tasks.Clear();
			//Invalidate();
		}
		
		public void addTask(uint index, string name)
		{
			tasks.Add(index, new GanttTask(name));
		}

		public void addSlice(uint index)
		{
			if (spans.Count > 0 && index == spans.Last().taskIndex)
			{
				spans.Last().duration+=5;
			}
			else
			{
				spans.Add(new GanttSpan() { taskIndex = index, start = sliceCount, duration = 5 });
			}
			sliceCount+=5;
			splt.Panel2.AutoScrollMinSize = new Size((int)sliceCount, 0);
			splt.Panel2.Invalidate();
		}

		public Gantt()
		{
			InitializeComponent();
			sf = new StringFormat();
			sf.LineAlignment = StringAlignment.Center;
		}

		private StringFormat sf;
		private void Gantt_Paint(object sender, PaintEventArgs e)
		{
			//Graphics g = e.Graphics;
			//float left = Math.Max(Width / 8, 50);
			//float rowHeight = this.Height / (tasks.Count + 1);
			//RectangleF box = new RectangleF(0, 0, left, rowHeight);
			//float endX = Width;

			//for (int i = 0; i < tasks.Count; ++i)
			//{
			//    box.Y = rowHeight * i;
			//    g.DrawString(tasks.Values[i].name, DefaultFont, Brushes.Black, box, sf);
			//}

			//for (int i = spans.Count - 1; i >= 0; --i)
			//{
			//    endX -= spans[i].duration;
			//    if (endX <= left)
			//    {
			//        g.FillRectangle(tasks[spans[i].taskIndex].brush, left, rowHeight * tasks.IndexOfKey(spans[i].taskIndex) + rowHeight / 4, spans[i].duration - (left - endX), rowHeight / 2);
			//        break;
			//    }
			//    g.FillRectangle(tasks[spans[i].taskIndex].brush, endX, rowHeight * tasks.IndexOfKey(spans[i].taskIndex) + rowHeight / 4, spans[i].duration, rowHeight / 2);
			//}
		}

		private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			float endX = sliceCount;
			float rowHeight = 50;
			
			g.TranslateTransform(splt.Panel2.AutoScrollPosition.X, splt.Panel2.AutoScrollPosition.Y);
			for (int i = spans.Count - 1; i >= 0; --i)
			{
				//if (spans[i].start >= splt.Panel2.AutoScrollPosition.X + splt.Panel2.Width)
				//	continue;

				endX -= spans[i].duration;
				if (endX <= 0)
				{
					g.FillRectangle(tasks[spans[i].taskIndex].brush, 0, rowHeight * tasks.IndexOfKey(spans[i].taskIndex) + rowHeight / 4, spans[i].duration + endX, rowHeight / 2);
					break;
				}
				g.FillRectangle(tasks[spans[i].taskIndex].brush, endX, rowHeight * tasks.IndexOfKey(spans[i].taskIndex) + rowHeight / 4, spans[i].duration, rowHeight / 2);
			}
		}

		private class GanttTask
		{
			private static readonly Random rand = new Random();
			public string name { get; set; }
			public Brush brush { get; set; }
			public GanttTask(string name)
			{
				this.name = name;
				brush = new SolidBrush(Color.FromArgb(rand.Next(128), rand.Next(128), rand.Next(128)));
			}
		}

		private class GanttSpan : IComparable<GanttSpan>
		{
			public uint taskIndex { get; set; }
			public float start { get; set; }
			public float duration { get; set; }

			public int CompareTo(GanttSpan other)
			{
				return start.CompareTo(other.start);
			}
		}
	}
}
