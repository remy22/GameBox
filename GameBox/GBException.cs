using System;
using System.Diagnostics;

namespace GameBox
{
	public class GBException : Exception
	{
		public enum Reason
		{
			XMLReading
		}

		public GBException (Reason reason)
		{
			Debug.Write("GBException: "+reason.ToString());
		}

		public GBException(Exception e):base(e.Message)
		{

		}
	}
}

