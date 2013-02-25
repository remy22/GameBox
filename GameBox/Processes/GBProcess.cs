using System;

namespace GameBox.Processes
{
    public class GBProcess
    {
    	public GBClient gbClient = null;

        internal void Start(GBClient gbClient_)
        {
        	GBDebug.Assert(gbClient_ != null);
        	gbClient = gbClient_;
        	OnStart();
        }
        
        public virtual void OnStart()
        {
        	
        }
        	
    }
}
