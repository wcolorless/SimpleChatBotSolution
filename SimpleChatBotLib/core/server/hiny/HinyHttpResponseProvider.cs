using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace WCOLORLESS.Hiny
{
    public class HinyHttpResponseProvider<TBase, TService>
    {
        TService service;
        public HinyHttpResponseProvider(TService service)
        {
            this.service = service;
        }


        public TBase Compute(TBase Params)
        {
            Type serviceType = service.GetType();
            var methods = serviceType.GetMethods();
            var MethodName = ((HinyTransportBaseObject)((object)Params)).MethodName;
            for (int i = 0; i < methods.Length; i++)
            {
                if(methods[i].Name == MethodName)
                {
                    var method = serviceType.GetMethod(methods[i].Name);
                    var sendObj = ((HinyTransportBaseObject)((object)Params)).Request;
                    var result = method.Invoke(service, new[] { sendObj });
                    ((HinyTransportBaseObject)((object)Params)).Response = result;
                    return Params;
                }
            }

            return default(TBase);
        }
    }
}
