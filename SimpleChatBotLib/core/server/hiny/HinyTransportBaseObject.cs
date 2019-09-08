using System;
using System.Collections.Generic;
using System.Text;

namespace WCOLORLESS.Hiny
{
    public class HinyTransportBaseObject
    {
        public string MethodName { get; set; }
        public object Request { get; set; }
        public object Response { get; set; }

        public HinyTransportBaseObject(string MethodName)
        {
            this.MethodName = MethodName;
        }

    }
}
