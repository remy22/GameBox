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
                DispatchActions(evnt);
            }
        }

        public void DispatchActions(GBEvent evnt)
        {
            if (eventsToProcess.Exists(evnt.EventType))
            {
                GBXMLContainer container = eventsToProcess[evnt.EventType];
                foreach (GBXMLContainer cnt in container.Children)
                {
                    DispatchAction(cnt);
                }
            }
        }

        public virtual void DispatchAction(GBXMLContainer actionData)
        {
        }
    }
}
