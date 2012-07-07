using System;

namespace GameBox
{
	namespace GBOD
	{
		internal class GBODToken
		{
			internal enum TokenType
			{
				OpenBracket,
				CloseBracket,
				Colon,
				ObjectName,
				ObjectValue,
				EOF,
				ErrorInvalidChar,
				ErrorEOFInMiddle,
			}

			private string TokenValue;
			private GBODChar First;
			private GBODChar Last;
			private TokenType Type;

			internal GBODToken(GBODChar first_, GBODChar last_, TokenType type_, string value)
			{
				First = first_;
				Last = last_;
				Type = type_;
				TokenValue = value;
			}
		}
	}
}

