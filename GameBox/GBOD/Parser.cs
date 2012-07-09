using System;
using GameBox.Utils.Log;

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
					ParseProperty(currentIndex, out lastParsed);
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

				currentIndex++;

				if (tList[currentIndex].Type != Token.TokenType.Colon)
				{
					lastParsed = currentIndex;
					return setError(-3);
				}

				// Now, there are 2 possibilites. An string or a new object definition.
				if (tList[currentIndex].Type == Token.TokenType.ObjectValue)
				{
					// Is a string.
				} else if (tList[currentIndex].Type == Token.TokenType.OpenBracket)
				{
					// Is a new object definition.
				} else {
					lastParsed = currentIndex;
					return setError(-4);
				}
			}
		}
	}
}

