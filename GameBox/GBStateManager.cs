using System;

namespace GameBox
{
	public class GBStateManager
	{
		private string[] states = null;
		private string current;

		public GBStateManager ()
		{
		}

		protected void RegisterStates(params string[] rStates)
		{
			GBDebug.Assert(rStates != null, "Not possible to register NULL states");
			GBDebug.Assert(rStates.Length > 1, "Not possible to register 0 or 1 states");

			states = (string[])rStates.Clone();
			current = states[0];
		}
	}
}

