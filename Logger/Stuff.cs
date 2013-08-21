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
}