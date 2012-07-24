using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBox.InternalServices
{
    internal class Singleton<T> where T : class, new()
    {
        private static T myInstance = null;

        internal static T create()
        {
            if (myInstance == null)
            {
                myInstance = new T();
            }
            return myInstance;
        }

        internal static T instance
        {
            get { return myInstance; }
        }

        internal virtual void Init()
        {
        }

        internal virtual void Destroy()
        {
        }

    }
}
