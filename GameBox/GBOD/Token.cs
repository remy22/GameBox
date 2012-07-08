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

			private string tokenValue;
			private Character first;
			private Character last;
			private TokenType type;

			internal Token(Character first_, Character last_, TokenType type_, string value_)
			{
				first = first_;
				last = last_;
				type = type_;
				tokenValue = value_;
			}

			internal bool Error
			{
				get { return type == TokenType.ErrorEOFInMiddle || type == TokenType.ErrorInvalidChar; }
			}

			public override string ToString ()
			{
				return string.Format ("[({0},{1}):{2}:{3}]",first.ToString(),last.ToString(),type.ToString(),tokenValue);
			}

			internal Character First
			{
				get { return first; }
			}

			internal Character Last
			{
				get { return last; }
			}

			internal TokenType Type
			{
				get { return type; }
			}

			internal string TokenValue
			{
				get { return tokenValue; }
			}
		}
	}
}

