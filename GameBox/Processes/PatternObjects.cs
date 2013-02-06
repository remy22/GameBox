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
            if (patternOrigin.Name.StartsWith("CopyPattern."))
			{
                string cpStr = "CopyPattern.";
                int cpLen = cpStr.Length;
                string originPattern = patternOrigin.Name.Substring(cpLen, patternOrigin.Name.Length - cpLen);
				GBXMLContainer retVal = (GBXMLContainer)data[originPattern].Clone();
                retVal.Overwrite(patternOrigin);
                if (patternOrigin.Exists("newName"))
                {
                    retVal.Name = patternOrigin["newName"].Text;
                }
				return retVal;
			}
			return patternOrigin;
		}

    }
}
