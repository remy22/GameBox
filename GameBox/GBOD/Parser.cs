using System;

namespace GameBox
{
	namespace GBOD
	{
		internal class Parser
		{
			private LogClient log = LogManager.getNewLogClient(typeof(Parser).ToString());
			private TokenList tList = null;

			internal Parser ()
			{
			}

			internal void Parse(TokenList tList_)
			{
				tList = tList_;
			}

			private int setError(int error_)
			{
				return error_;
			}

			internal int ParseObject(int start, out int lastParsed)
			{
				// { PropertiesDeclaration }
				int currentIndex = start;

				if (tList[currentIndex].Type != Token.TokenType.OpenBracket)
				{
					lastParsed = currentIndex;
					return setError(-1);

				}

				currentIndex++;

				while (tList[currentIndex].Type == Token.TokenType.ObjectName)
				{
					ParseProperty();
				}
			}

			internal int ParseProperty(int start,out int lastParsed)
			{
				// PropertyName: ["string" || Object]
				int currentIndex = start;

				if (tList[currentIndex].Type != Token.TokenType.ObjectName)
				{
					lastParsed = currentIndex;
					return setError(-2);

				}
			}
		}
	}
}

