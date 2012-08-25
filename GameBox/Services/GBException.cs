using System;

namespace GameBox.Services
{
	public class GBException : Exception
	{
		public GBException () : base()
		{
		}

		public GBException (Exception e) : base(e.Message, e)
		{
		}

		public static GBException create(Exception other)
		{
			return new GBException(other);
		}
	}
}

