using System;
using GameBox.XMLSerialization;

namespace GameBox.Events
{
    public class GBEventReceiver
    {
        protected GBXMLContainer eventsToProcess = null;

        public GBEventReceiver(GBXMLContainer initData)
        {
            eventsToProcess = (GBXMLContainer)initData["ReceiveEvents"].Clone();
        }

        public virtual void DispatchGBEvent(GBEvent evnt)
        {
            DispatchActions(evnt);
        }

        public void DispatchActions(GBEvent evnt)
        {
            if (eventsToProcess.Exists(evnt.EventType))
            {
                if (eventsToProcess[evnt.EventType].Exists("PerformAction"))
                {
                    GBXMLContainer container = eventsToProcess[evnt.EventType]["PerformAction"];
                    DispatchAction(container);
                }
            }
        }

        public virtual void DispatchAction(GBXMLContainer actionData)
        {
        }
    }
}
