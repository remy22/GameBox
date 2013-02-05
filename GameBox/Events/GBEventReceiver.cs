using System;
using GameBox.XMLSerialization;
using System.Collections.Generic;
using GameBox.Processes;

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
                    GBXMLContainer containerTemp = eventsToProcess[evnt.EventSubType];
                    bool isValid = true;
                    foreach (GBXMLContainer ch in containerTemp.Children)
                    {

                        if (!ch.Name.Equals("Action"))
                        {
                            if (evnt.data.Exists(ch.Name))
                            {
                                if (evnt.data[ch.Name].Text.Equals(ch.Text))
                                {
                                }
                                else
                                {
                                    isValid = false;
                                }
                            }
                            else
                            {
                                isValid = false;
                            }
                        }
                    }

                    if (isValid)
                    {
                        string action = containerTemp["Action"].Text;

                        if (!action.Equals(string.Empty))
                        {
                            if (action.StartsWith("Process."))
                            {
                                if (ProcessManager.ActiveProcess != null)
                                {
                                    ProcessManager.ActiveProcess.DispatchAction(evnt, action);
                                }
                                else
                                {
                                    GBDebug.WriteLine("Ignoring process action (" + action + ") because there is no active process");
                                }
                            }
                            DispatchAction(evnt, action);
                        }
                    }
				}
            }
        }

        public virtual void DispatchAction(GBEvent evnt, string action)
        {
        }
    }
}
