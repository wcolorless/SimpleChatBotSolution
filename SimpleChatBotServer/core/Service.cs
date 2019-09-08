using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WCOLORLESS.Hiny;
using SimpleChatBotLib;

namespace SimpleChatBotServer
{
    public enum CredentialsOpTypes
    {
        Empty,
        Add,
        Remove
    }

    public class Service
    {

        private Chat _ChatBot;
        public Func<ChatBotCredentials, CredentialsOpTypes, bool> UpdateCredentialsList;


        public Service()
        {
            _ChatBot = new Chat();
        }

        public List<IChatCommand> Commands
        {
            get { return _ChatBot.Commands; }
        }

        public Message Login(ChatBotCredentials credentials)
        {
            var logger = Logger.GetInstance();
            logger.AddItem("Attempt to Login from: " + credentials.Name);
            var result = UpdateCredentialsList(credentials, CredentialsOpTypes.Add);
            if(result)
            {
                var AnswerMessage = _ChatBot.GetAnwser(new Message() { Receiver = "Bot", Sender = credentials.Name, Text = "!help" });
                return new Message() { Receiver = credentials.Name, Sender = "Bot", Text = "Hello " + credentials.Name + "! I Am Bot!" + "\n" + AnswerMessage.Text };
            }
            else return new Message() { Receiver = credentials.Name, Sender = "Bot", Text = "Your name already exist" };
        }

        public bool Logout(ChatBotCredentials credentials)
        {
            var logger = Logger.GetInstance();
            logger.AddItem("Attempt to Logout from: " + credentials.Name);
            var result = UpdateCredentialsList(credentials, CredentialsOpTypes.Remove);
            return result;
        }

        public Message SendMessage(Message message)
        {
            var logger = Logger.GetInstance();
            logger.AddItem("Message from: " + message.Sender + "; Text: " + message.Text);
            var AnswerMessage = _ChatBot.GetAnwser(message);
            return AnswerMessage;
        }
    }
}
