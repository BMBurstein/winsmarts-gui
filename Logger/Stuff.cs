using System;
using System.Windows.Forms;
using System.Reflection;

namespace Logger
{
	public enum TaskStates
	{
		READY,
		NOT_ACTIVE,
		SUSPENDED,
		SLEEPING,
		UNKNOWN
	}

	public enum LogMsg
	{
		LOG_START,
		LOG_END,
		LOG_NEW_TASK,
		LOG_CONTEXT_SWITCH,
		LOG_CONTEXT_SWITCH_ON,
		LOG_CONTEXT_SWITCH_OFF,
		LOG_TIMER,

		LOG_TASK_STATUS_CHANGE,
	};

	public class Task
	{
		public uint priority { get; set; }
		public string name { get; set; }
		public uint tid { get; set; }
		public TaskStates state { get; set; }
	}

	public class LogEntry : IComparable<LogEntry>
	{
		public LogMsg msg { get; set; }
		public uint num { get; set; }
		public string[] props { get; set; }

		public int CompareTo(LogEntry other)
		{
			return num.CompareTo(other.num);
		}
	}

	public static class MyExtensions
	{
		public static byte[] ToByteArray(this string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		public static void DoubleBuffered(this Control control, bool enable)
		{
			var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			doubleBufferPropertyInfo.SetValue(control, enable, null);
		}
	}

	public enum DEBUG_COMMANDS
	{
		PAUSE,
		CONTINUE,
		GET_ALL,
	};
}