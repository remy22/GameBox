using System;
using GameBox.XMLSerialization;
using System.Collections.Generic;

namespace GameBox.Events
{
    public class GBEventReceiver
    {
        protected GBXMLContainer eventsToProcess = null;

        public GBEventReceiver(GBXMLContainer initData)
        {
            eventsToProcess = (GBXMLContainer)initData["ReceiveEvents"].Clone();
        }

        public virtual void DispatchGBEvents(List<GBEvent> evntList)
        {
            foreach (GBEvent evnt in evntList)
            {
                switch (evnt.EventType)
                {
                    case "Actions":
                        DispatchActions(evnt);
                        break;
                    case "Keyboard":
                        break;
                }
            }
        }

        public void DispatchActions(GBEvent evnt)
        {
            if (eventsToProcess.Exists(evnt.EventSubType))
            {
                string action = eventsToProcess[evnt.EventSubType]["Action"].Text;
                DispatchAction(evnt, action);
            }
        }

        public virtual void DispatchAction(GBEvent evnt, string action)
        {
        }
    }
}
