using System;
using System.IO;

namespace GameBox
{
	namespace GBOD
	{
		public class Stream
		{
			private StreamReader reader = null;
			private ushort x;
			private uint y;

			public Stream (StreamReader reader_)
			{
				reader = reader_;
			}

			public void Reset()
			{
				x = 0;
				y = 0;
			}

			public Character getNext()
			{
				char ch = (char)reader.Read();
				return new Character(x, y, ch);
			}

			public bool EOF
			{
				get { return reader.EndOfStream; }
			}
		}
	}
}

