using System;

namespace GameBox
{
	namespace GBOD
	{
		internal class Token
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
			private Character First;
			private Character Last;
			private TokenType Type;

			internal Token(Character first_, Character last_, TokenType type_, string value)
			{
				First = first_;
				Last = last_;
				Type = type_;
				TokenValue = value;
			}
		}
	}
}

