using System;
using System.Collections.Generic;
using GameBox.XMLSerialization;

namespace GameBox.Processes
{
    public class PatternObjects
    {
        private GBXMLContainer data = new GBXMLContainer();

        public PatternObjects()
        {
        }

        public void AddObject(GBXMLContainer newObject)
        {
            data.AddChild(newObject);
        }

		internal GBXMLContainer ParsePattern(GBXMLContainer patternOrigin)
		{
			string cpStr = "CopyPattern.";
			int cpLen = cpStr.Length;
			if (patternOrigin.Name.StartsWith(cpStr))
			{
				string originPattern = patternOrigin.Name.Substring(cpLen, patternOrigin.Name.Length - cpLen);
				GBXMLContainer retVal = (GBXMLContainer)data[originPattern].Clone();
				return retVal;
			}
			return patternOrigin;
		}

    }
}
