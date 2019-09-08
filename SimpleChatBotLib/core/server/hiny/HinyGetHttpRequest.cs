using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WCOLORLESS.Hiny
{
    public class HinyGetHttpRequest
    {

        public static T GetParams<T>(string Request)
        {
            var settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            var obj = JsonConvert.DeserializeObject<T>(Request, settings);
            return obj;
        }
    }
}
