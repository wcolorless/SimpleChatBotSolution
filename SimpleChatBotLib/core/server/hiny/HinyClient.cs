using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;



namespace WCOLORLESS.Hiny
{
    public class HinyClient
    {
        private HinyTransport hinyTransport;

        public HinyClient(string address)
        {
            hinyTransport = new HinyTransport(address);
        }

        public async Task<T> Request<T>(T requestObject)
        {
            var indented = Formatting.Indented;
            var settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            var req = JsonConvert.SerializeObject(requestObject, indented, settings);
            var response = await hinyTransport.SendRequest(req);
            var res = JsonConvert.DeserializeObject<T>(response, settings);
            return res;
        }
    }
}
