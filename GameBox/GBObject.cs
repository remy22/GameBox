using System;
using GameBox.XMLSerialization;

namespace GameBox
{
	public class GBObject
	{
		protected GBXMLContainer StartUpProperties = null;

		public GBObject ()
		{
		}

		public GBObject(GBXMLContainer containter)
		{
			StartUpProperties = containter.Clone() as GBXMLContainer;
			PreInit();
		}

		public virtual void PreInit()
		{
			// The properties are set.
		}



	}
}

