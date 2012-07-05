using System;
using System.IO;

namespace GaBoxOD
{
	public class GBODStream
	{
		private StreamReader reader = null;
		private ushort x;
		private uint y;

		public GBODStream (StreamReader reader_)
		{
			reader = reader_;
		}

		public void Reset()
		{
			x = 0;
			y = 0;
		}

		public GBODChar getNext()
		{
			char ch = (char)reader.Read();
			return new GBODChar(x, y, ch);
		}

		public bool EOF
		{
			get { return reader.EndOfStream; }
		}
	}
}

