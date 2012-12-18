using System;
using System.Diagnostics;

namespace GameBox
{
	public class GBException : Exception
	{
		public enum Reason
		{
			XMLReading,
            UnableToLoadModule,
            FileNotFound
		}

		public GBException (Reason reason)
		{
			Debug.Write("GBException: "+reason.ToString());
		}

        public GBException(Reason reason, Exception e)
            : base(e.Message)
        {

        }

		public GBException(Exception e)
            : base(e.Message)
		{

		}
	}
}

