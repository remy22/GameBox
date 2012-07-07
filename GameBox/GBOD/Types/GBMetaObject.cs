using System;
using System.Collections.Generic;

namespace GameBox
{
	namespace GBOD
	{
		namespace Types
		{
			public class GBMetaObject : GBObject
			{
				private Dictionary<string,GBObject> properties = new Dictionary<string,GBObject>();

				public GBMetaObject ()
				{
				}
			}
		}
	}
}

