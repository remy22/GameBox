using System;
using System.Collections.Generic;

namespace GameBox.XMLSerialization
{
    public static class XMLSerializeContext
    {
        public static List<string> resourceList = new List<string>();

        public static void Reset()
        {
   		    resourceList = new List<string>();
        }
    }
}
