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

        public GBEvent(params string[] properties)
        {
            data = GBXMLContainer.LoadFromProperties(properties);
            GBDebug.WriteLine("New event created:"+data);
        }

        public string EventType
        {
            get { return data.Name; }
        }
    }
}
