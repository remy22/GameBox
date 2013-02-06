using System;
using GameBox.XMLSerialization;
using System.Collections.Generic;
using GameBox.Processes;
using GameBox.Graphics;

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
                string evntComplete = evnt.EventType + "." + evnt.EventSubType;
				if (eventsToProcess.Exists(evntComplete))
				{
                    GBXMLContainer containerTemp = eventsToProcess[evntComplete];
                    bool isValid = true;
                    foreach (GBXMLContainer ch in containerTemp.Children)
                    {

                        if (!ch.Name.StartsWith("Action"))
                        {
                            if (ch.Name.StartsWith("RequireProperty."))
                            {
                                string[] element = ch.Name.Split('.');
                                RenderNode nodeToCheck = null;
                                if (element[1].Equals("Scene"))
                                {
                                    nodeToCheck = ProcessManager.ActiveProcess.CurrentScene;
                                }
                                else
                                {
                                    //                                    nodeToCheck = this;
                                }

                                if (nodeToCheck != null)
                                {
                                    string val = nodeToCheck.getProperty(element[2]).Text;
                                    if (!ch.Text.Equals(val))
                                    {
                                        isValid = false;
                                    }
                                }
                                else
                                {
                                    isValid = false;
                                }

                            }
                            else
                            {
                                if (!evnt.data.Exists(ch.Name) || !evnt.data[ch.Name].Text.Equals(ch.Text))
                                {
                                    isValid = false;
                                }
                            }
                        }
                    }

                    if (isValid)
                    {
                        string action = containerTemp["Action"].Text;

                        if (!action.Equals(string.Empty))
                        {
                            GBXMLContainer actionData = containerTemp.getPropertiesStartingWith("Action");
                            if (action.StartsWith("Process."))
                            {
                                if (ProcessManager.ActiveProcess != null)
                                {
                                    ProcessManager.ActiveProcess.DispatchAction(evnt, action, actionData);
                                }
                                else
                                {
                                    GBDebug.WriteLine("Ignoring process action (" + action + ") because there is no active process");
                                }
                            }
                            DispatchAction(evnt, action, actionData);
                        }
                    }
				}
            }
        }

        public virtual void DispatchAction(GBEvent evnt, string action,GBXMLContainer actionData)
        {
        }
    }
}
