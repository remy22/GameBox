using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBox.Context
{
    internal class Process
    {
        private ProcessContext parentContext;

        internal protected Process(ProcessContext e)
        {
            parentContext = e;
        }

        internal virtual void OnInit()
        {
        }

        internal virtual void OnStart()
        {
        }
    }
}
