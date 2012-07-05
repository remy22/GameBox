using System;

namespace GaBoxOD
{
	public class GBOD
	{
		static GBOD instance = null;

		private GBOD ()
		{
		}

		public static GBOD getNewInstance()
		{
			if (instance == null)
				return instance;

			return instance = new GBOD();
		}
	}
}

