using System;

namespace GameBox
{
	namespace Utils
	{
		namespace Log
		{
			internal class LogClient
			{
				private string objectName;
				private string preface;

				internal LogClient (string objName)
				{
					objectName = objName;
					preface = objectName + ": ";
				}

				private void write(string value_)
				{
				}

				private string completeString(string value_)
				{
					return preface + value_;
				}

				public void debug(string value_)
				{
					logDebug(value_);
				}

				public void logDebug(string value_)
				{
					LogManager.write(false, LogManager.LogType.Log, completeString(value_));
				}

				public void infoDebug(string value_)
				{
					LogManager.write(false, LogManager.LogType.Info, completeString(value_));
				}

				public void errorDebug(string value_)
				{
					LogManager.write(false, LogManager.LogType.Error, completeString(value_));
				}

				public void logRelease(string value_)
				{
					LogManager.write(true, LogManager.LogType.Log, value_);
				}

				public void infoRelease(string value_)
				{
					LogManager.write(true, LogManager.LogType.Info, value_);
				}

				public void errorRelease(string value_)
				{
					LogManager.write(true, LogManager.LogType.Error, value_);
				}

			}
		}
	}
}

