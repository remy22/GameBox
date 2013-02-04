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
				if (eventsToProcess.Exists(evnt.EventSubType))
				{
					if (eventsToProcess[evnt.EventSubType].Exists("Action"))
					{
						DispatchAction(evnt, eventsToProcess[evnt.EventSubType]["Action"].Text);
					}
				}
            }
        }

        public virtual void DispatchAction(GBEvent evnt, string action)
        {
        }
    }
}
