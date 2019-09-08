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
using System.Net;
using System.Net.Http;


namespace WCOLORLESS.Hiny
{
    public class HinyServer<TBase, TService>
    {
        private bool IsRun;
        private HttpListener _Listener;
        private HinyHttpResponseProvider<TBase, TService> _httpResponseProvider;
        private Thread _myThread;
        public HinyServer(string address, HinyHttpResponseProvider<TBase, TService> httpResponseProvider)
        {
            _httpResponseProvider = httpResponseProvider;
            _Listener = new HttpListener();
            _Listener.Prefixes.Add(address);
        }

        public void Start()
        {
            _myThread = new Thread(StartListen);
            _myThread.Start();
        }


        private async void StartListen()
        {
            if (!IsRun)
            {
                try
                {
                    _Listener.Start();
                    IsRun = true;
                    while (true)
                    {
                        string requestString;
                        HttpListenerContext CurrentContext;
                        CurrentContext = await _Listener.GetContextAsync();
                        var Request = CurrentContext.Request;
                        if (Request.HttpMethod == "POST")
                        {
                            var GetRequest = Request.Url.LocalPath;
                            using (System.IO.Stream body = Request.InputStream)
                            {
                                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, Request.ContentEncoding))
                                {
                                    requestString = reader.ReadToEnd();
                                }
                            }
                            var Params = HinyGetHttpRequest.GetParams<TBase>(requestString);
                            var answer = _httpResponseProvider.Compute(Params);
                            var response = CurrentContext.Response;
                            var responseStream = response.OutputStream;
                            var indented = Formatting.Indented;
                            var settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
                            var data = JsonConvert.SerializeObject(answer, indented, settings);
                            var returnArray = Encoding.UTF8.GetBytes(data);
                            response.ContentLength64 = returnArray.Length;
                            responseStream.Write(returnArray, 0, returnArray.Length);
                        }
                    }
                }
                catch (Exception e)
                {
                   if(e.Message != "The operation was canceled.") throw new Exception("Sorry Hiny couldn't handle it: " + e.Message);
                }
            }
        }
    }
}
