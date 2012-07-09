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
				private Dictionary<string,object> properties = new Dictionary<string,object>();

				public GBMetaObject ()
				{
				}

				public bool AddString(string key, string value)
				{
					if (properties.ContainsKey(key))
					{
						return false;
					}

					properties[key] = value;
					return true;
				}

				public bool AddObject(string key, GBMetaObject value)
				{
					if (properties.ContainsKey(key))
					{
						return false;
					}

					properties[key] = value;
					return true;
				}

				public bool KeyExists(string key)
				{
					return properties.ContainsKey(key);
				}

				public bool isString(string key)
				{
					if (properties.ContainsKey(key))
					{
						if (properties[key].GetType() == typeof(string))
							return true;
					}
					return false;
				}

			}
		}
	}
}

