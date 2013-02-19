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
		private List<string> names = new List<string>();
		private SortedList<uint, int> indexes = new SortedList<uint, int>();
		private uint sliceCount = 0;

        public void reset()
        {
            //spans.Clear(); 
            names.Clear();
            indexes.Clear();
        }
		
        public void addTask(uint index, string name)
		{
			names.Add(name);
			indexes.Add(index, names.Count - 1);
		}

		//public void addSpan(uint index, float start, float duration)
		//{
		//    GanttSpan span = new GanttSpan() { taskIndex = index, start = start, duration = duration };
		//    if (spans.Count > 1 && span.CompareTo(spans.Last()) < 0)
		//    {
		//        spans.Add(span);
		//        spans.Sort();
		//    }
		//    else
		//    {
		//        spans.Add(span);
		//    }
		//}

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
			Invalidate();
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
			Graphics g = e.Graphics;
			float left = Math.Max(Width / 8, 50);
			float rowHeight = this.Height / (names.Count + 1);
			RectangleF box = new RectangleF(0, 0, left, rowHeight);
			float endX = Width;

			for (int i = 0; i < names.Count; ++i)
			{
				box.Y = rowHeight * i;
				g.DrawString(names[i], DefaultFont, Brushes.Black, box, sf);
			}

			for (int i = spans.Count - 1; i >= 0; --i)
			{
				endX -= spans[i].duration;
				if (endX <= left)
				{
					g.FillRectangle(Brushes.LightGreen, left, rowHeight * indexes[spans[i].taskIndex] + rowHeight / 4, spans[i].duration - (left - endX), rowHeight / 2);
					break;
				}
				g.FillRectangle(Brushes.LightGreen, endX, rowHeight * indexes[spans[i].taskIndex] + rowHeight / 4, spans[i].duration, rowHeight / 2);
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
