using System;

namespace GameBox
{
	namespace GBOD
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
}

