using System;
using GameBox.XMLSerialization;

namespace GameBox.Events
{
    public class GBEvent
    {
        public GBXMLContainer data;
        private string environment;
        private string type;
        private string subType;

        public GBEvent(GBXMLContainer data_)
        {
            data = (GBXMLContainer)data_.Clone();
        }

        public GBEvent(params string[] properties)
        {
            data = GBXMLContainer.LoadFromProperties(properties);
            SetProperties();
        }

        private void SetProperties()
        {
            string[] types = data.Name.Split('.');
            environment = types[0];
            type = (types.Length > 1) ? types[1] : "";
            subType = (types.Length > 2) ? types[2] : "";
            GBDebug.WriteLine("New event created:"+data);
        }

        public string Environment
        {
            get { return environment; }
        }

        public string EventType
        {
            get { return type; }
        }

        public string EventSubType
        {
            get { return subType; }
        }
    }
}
