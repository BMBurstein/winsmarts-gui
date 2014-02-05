using System;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Logger
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
		STEP,
		SET_TASK,
		SET_CS,
	};

	//These two clases were taken from http://www.differentpla.net/content/2005/02/using-propertygrid-with-dictionary
	class DictionaryPropertyGridAdapter : ICustomTypeDescriptor
	{
		IDictionary _dictionary;

		public DictionaryPropertyGridAdapter(IDictionary d)
		{
			_dictionary = d;
		}

		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return _dictionary;
		}

		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return null;
		}

		PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			ArrayList properties = new ArrayList();
			foreach (DictionaryEntry e in _dictionary)
			{
				properties.Add(new DictionaryPropertyDescriptor(_dictionary, e.Key));
			}

			PropertyDescriptor[] props =
				(PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor));

			return new PropertyDescriptorCollection(props);
		}
	}
	class DictionaryPropertyDescriptor : PropertyDescriptor
	{
		IDictionary _dictionary;
		object _key;

		internal DictionaryPropertyDescriptor(IDictionary d, object key)
			: base(key.ToString(), null)
		{
			_dictionary = d;
			_key = key;
		}

		public override Type PropertyType
		{
			get { return _dictionary[_key].GetType(); }
		}

		public override void SetValue(object component, object value)
		{
			_dictionary[_key] = value;
		}

		public override object GetValue(object component)
		{
			return _dictionary[_key];
		}

		public override bool IsReadOnly
		{
			get { return false; }
		}

		public override Type ComponentType
		{
			get { return null; }
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override void ResetValue(object component)
		{
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}