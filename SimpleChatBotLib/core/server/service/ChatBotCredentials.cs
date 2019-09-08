using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SimpleChatBotLib
{
    [JsonObject]
    public class ChatBotCredentials
    {
        private string _Name;
        
        
        public string Name
        {
            get { return _Name; }
            set
            {
                if(_Name != value)
                {
                    _Name = value;
                }
            }
        }

        public ChatBotCredentials()
        {

        }
    }
}
