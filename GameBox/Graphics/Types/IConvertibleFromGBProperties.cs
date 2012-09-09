using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameBox.Graphics.Types
{
    interface IConvertibleFromGBProperties
    {
        void CreateFromProperties(GBProperties source);
    }
}
