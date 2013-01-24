using System;
using GameBox.XMLSerialization;

namespace GameBox.Events
{
    public class GBEvent
    {
        public GBXMLContainer data;

        public GBEvent(GBXMLContainer data_)
        {
            data = (GBXMLContainer)data_.Clone();
        }

        public string EventType
        {
            get { return data.Name; }
        }
    }
}
