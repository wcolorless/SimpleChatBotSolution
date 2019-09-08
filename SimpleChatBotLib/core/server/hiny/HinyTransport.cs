using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WCOLORLESS.Hiny
{
    public class HinyTransport
    {
        private string _address;

        public HinyTransport(string address)
        {
            _address = address;
        }


        public async Task<string> SendRequest(string req)
        {
            var request = new HttpRequestMessage();
            var adrstr = _address;
            var array = Encoding.ASCII.GetBytes(req);
            HttpContent newContent = new ByteArrayContent(array);
            request.RequestUri = new Uri(adrstr);
            request.Method = HttpMethod.Post;
            request.Content = newContent;
            request.Headers.Add("Accept", "application/json");
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(3);
                var result = await client.SendAsync(request);
                var content = result.Content;
                var message = await content.ReadAsStringAsync();
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return message;
                }
                else return "Error: Bad Response: " + message;
            }
            catch (Exception e)
            {
                return "Error: " + e.Message;
            }
        }
    }
}
