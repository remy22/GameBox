using System;

namespace GameBox
{
	namespace GBOD
	{
		public class Character : ICloneable
		{
			private char ch;
			private ushort x;
			private uint y;

			internal Character (ushort x_, uint y_, char ch_)
			{
				x = x_;
				y = y_;
				ch = ch_;
			}

			public char Ch
			{
				get { return ch; }
			}

			public ushort X
			{
				get { return x; }
			}

			public uint Y
			{
				get { return y; }
			}

			public override string ToString ()
			{
				return string.Format ("[GBODChar: Ch={0}, X={1}, Y={2}]", Ch, X, Y);
			}

			public bool IsAlpha
			{
				get
				{
					return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z');
				}
			}

			public bool IsNumeric
			{
				get
				{
					return (ch >= '0' && ch <= '9');
				}
			}

			public bool IsAlphaNumeric
			{
				get
				{
					return IsAlpha || IsNumeric;
				}
			}

			#region ICloneable implementation
			public object Clone ()
			{
				return new Character(x, y, ch);
			}
			#endregion

		}
	}
}

