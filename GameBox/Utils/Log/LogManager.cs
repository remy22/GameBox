using System;

namespace GameBox
{
	namespace Utils
	{
		namespace Log
		{
			internal class LogManager
			{
				internal enum LogType
				{
					Log,
					Info,
					Error,
				}

				private static LogManager instance = null;

				private LogManager ()
				{
				}

				internal static LogManager Create()
				{
					if (instance == null)
					{
						instance = new LogManager();
					}

					return instance;
				}

				internal static LogManager Instance
				{
					get { return instance; }
				}

				internal static LogClient getNewLogClient(string objectName)
				{
					return new LogClient(objectName);
				}

				internal static void write(bool release_,LogType type, string str_)
				{
					Console.WriteLine(str_);
				}
			}
		}
	}
}

