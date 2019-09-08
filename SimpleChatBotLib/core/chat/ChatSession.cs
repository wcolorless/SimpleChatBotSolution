using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleChatBotLib 
{
    public class ChatSession
    {
        private string _ClientName;
        private List<IMessage> messages = new List<IMessage>();


        public string ClientName
        {
            get { return _ClientName; }
        }


        private ChatSession(string ClientName)
        {
            _ClientName = ClientName;
        }

        public static ChatSession Create(string ClientName)
        {
            return new ChatSession(ClientName);
        }


        public void AddMessage(IMessage message)
        {
            messages.Add(message);
        }

    }
}
