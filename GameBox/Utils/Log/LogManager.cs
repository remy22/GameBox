using System;

namespace GameBox
{
	namespace Utils
	{
		namespace Log
		{
			internal class LogManager
			{
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

			}
		}
	}
}

