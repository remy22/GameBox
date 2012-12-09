using GameBox.XMLSerialization;

namespace GameBox
{
	public static class GBProperties
	{
		private static GBXMLContainer generalProperties;

		public static void Init ()
		{
			generalProperties = GBXMLContainer.LoadOrNull ("properties.xml");

			GBDebug.WriteLineIf (generalProperties == null, "Properties file not found. Using defaults");

            if (generalProperties == null)
            {
                // Create the default properties.
                generalProperties = GBXMLContainer.LoadFromString(
                    "<properties>" +
						"<window>" +
                        	"<screenResolution type=\"Vector3\">" +
                            	"<x>800</x>" +
                            	"<y>600</y>" +
							"</screenResolution>" +
							"<bpp>32</bpp>" +
						"</window>" +
                    "</properties>"
                    );
            }

            GBDebug.WriteLine("Properties:");
            GBDebug.WriteLine(generalProperties);
		}

		public static GBXMLContainer Properties
		{
			get { return generalProperties; }
		}
	}
}

