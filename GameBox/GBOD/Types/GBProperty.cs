using System;

namespace GameBox
{
	namespace GBOD
	{
		namespace Types
		{
			public class GBProperty : GBObject
			{
				private string key;
				private string val;

				public GBProperty (string key_,string value_)
				{
					key = key_;
					val = value_;
				}

				public string Key
				{
					get { return key; }
				}

				public string Value
				{
					get { return val; }
					set { val = value; }
				}
			}
		}
	}
}

