using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Philosophers
{
	public enum TaskStates
	{
		READY,
		NOT_ACTIVE,
		SUSPENDED,
		SLEEPING,
		RUNNING,
		UNKNOWN,
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
		LOG_DEADLOCK,
		LOG_TIME_OUT,
		LOG_REDECLARE,

		LOG_TASK_STATUS_CHANGE,
		LOG_TASK_PROP_SET,

		LOG_LOCK_ACQUIRE,
		LOG_LOCK_RELEASE,
		LOG_LOCK_WAIT,
		LOG_LOCK_COUNT,
	};

	public class Task
	{
		public uint priority { get; set; }
		public string name { get; set; }
		public uint tid { get; set; }
		public TaskStates state { get; set; }

		public const uint NO_TASK = uint.MaxValue;
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
	}
}
