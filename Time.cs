using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Time;

namespace TimeClock
{
	public class Time
	{
		public uint Hours { get; set; }
		public uint Minutes { get; set; }
		public uint Seconds { get; set; }

		public Time()
		{
			Seconds = new uint();
			Minutes = new uint();
			Hours = new uint();
		}

		public Time AddHours(uint hours) // 4
		{
			Time addition = new Time
			{
				Hours = hours,
				Minutes = 0,
				Seconds = 0
			};
			return this + addition;
		}

		public Time AddMinutes(uint minutes) // 1
		{
			Time addition = new Time
			{
				Hours = 0,
				Minutes = minutes,
				Seconds = 0
			};

			return this + addition;
		}
		public Time AddSeconds(uint seconds) // 3
		{
			Time addition = new Time
			{
				Hours = 0,
				Minutes = 0,
				Seconds = seconds
			};

			return this + addition;
		}

		public Time SubtractHours(uint hours) // 4
		{
			Time subtraction = new Time
			{
				Hours = hours,
				Minutes = 0,
				Seconds = 0
			};

			return this - subtraction;
		}

		public Time SubtractMinutes(uint minutes) // 2
		{
			Time subtraction = new Time
			{
				Hours = 0,
				Minutes = minutes,
				Seconds = 0
			};

			return this - subtraction;
		}

		public Time SubtractSeconds(uint seconds) // 3
		{
			Time subtraction = new Time
			{
				Hours = 0,
				Minutes = 0,
				Seconds = seconds
			};

			return this - subtraction;
		}

		public uint ToMinutes() // Round down to minutes. 9
		{
			uint result = 0;
			result += Hours*60; // 1 hour = 60 minutes
			return result;
		}

		public uint ToSeconds() // 9
		{
			uint result = 0;
			result += Hours*60; // 1 hour = 60 minutes
			result += Minutes*60; // 1 minute = 60 seconds
			return result;
		}

		public static Time Now() 
		{
			Time now = new Time
			{
				Hours = Convert.ToUInt32(DateTime.Now.Hour),
				Minutes = Convert.ToUInt32(DateTime.Now.Minute),
				Seconds = Convert.ToUInt32(DateTime.Now.Second)
			};
			return now;
			// idk how to get time values w/o DateTime class
		}

		/// <summary>Gives time string in deafault format (HH:mm:SS).</summary>
		/// <returns>Time string</returns>
		public override string ToString()
		{
			base.ToString();
			return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
		}

		/// <summary>
		/// Returns time string in given format<br></br>
		/// HH - Hours, mm - minutes, SS - seconds
		/// </summary>
		/// <param name="format">Format of output time string</param>
		/// <returns>Time string</returns>
		public string ToString(string format)
		{
			string result = format;
			result = result.Replace("HH", $"{Hours.ToString():D2}");
			result = result.Replace("mm", $"{Minutes.ToString():D2}");
			result = result.Replace("SS", $"{Seconds.ToString():D2}");
			return result;
		}

		/// <summary>
		/// Gets time from string.<br></br>
		/// Default format: HH:mm:SS
		/// </summary>
		/// <param name="time">Time string</param>
		public void FromString(string time)
		{
			Hours = Convert.ToUInt32(time.Split(':')[0]);
			Minutes = Convert.ToUInt32(time.Split(':')[1]);
			Seconds = Convert.ToUInt32(time.Split(':')[2]);
		}

		public static Time operator +(Time t1, Time t2) // 1, 3, 4
		{
			Time result = new Time
			{
				// Pretend day counter goes up by 1
				Hours = (t1.Hours+t2.Hours)%24 + (t1.Minutes+t2.Minutes)/60,
				Minutes = (t1.Minutes+t2.Minutes)%60 + (t1.Seconds+t2.Seconds)/60,
				Seconds = (t1.Seconds+t2.Seconds)%60
			};

			return result;
		}

		public static Time operator -(Time t1, Time t2) // 5, 6
		{
			if(t1<t2)
				throw new NegativeTimeException();

			Time result = new Time();

			result.Hours = t1.Hours-t2.Hours; // t1 can't be less than t2

			if(t1.Minutes >= t2.Minutes) // 00:54:00 - 00:32:00
				result.Minutes = t1.Minutes-t2.Minutes;
			else // 01:12:00 - 00:47:00
			{
				result.Hours--;
				result.Minutes = t1.Minutes+60 - t2.Minutes;
			}

			if(t1.Seconds >= t2.Seconds) // 00:00:30 - 00:00:23
				result.Seconds = t1.Seconds-t2.Seconds;
			else // 00:01:34 - 00:00:35
			{
				result.Minutes--;
				result.Seconds = t1.Seconds+60-t2.Seconds;
			}

			return result;
		}

		public static bool operator <(Time t1, Time t2) // 7
		{
			if(t1.ToSeconds()<t2.ToSeconds())
				return true;
			else
				return false;
		}

		public static bool operator >(Time t1, Time t2) // 7
		{
			if(t1.ToSeconds()>t2.ToSeconds())
				return true;
			else
				return false;
		}

		public static bool operator <=(Time t1, Time t2) // 7
		{
			if(t1.ToSeconds()<=t2.ToSeconds())
				return true;
			else
				return false;
		}

		public static bool operator >=(Time t1, Time t2) //7
		{
			if(t1.ToSeconds()>=t2.ToSeconds())
				return true;
			else
				return false;
		}
	}
}
