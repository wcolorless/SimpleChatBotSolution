using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SimpleChatBotLib
{
    public class Chat
    {
        private List<ChatSession> _sessions = new List<ChatSession>();
        private List<IChatCommand> _commands = new List<IChatCommand>();


        public Chat()
        {
            _commands.Add(new HelpChatCommand(_commands));
        }

        public List<IChatCommand> Commands
        {
            get { return _commands; }
        }




        public Message GetAnwser(Message message)
        {
            Message newMessage;
            var command = FindCommand(message.Text);
            if(command != "Empty" && !string.IsNullOrEmpty(command))
            {
                var selectedCommand = _commands.Find(x => x.Command == command);
                if(selectedCommand != null)
                {
                    newMessage = new Message() { Receiver = message.Sender, Sender = "Bot", Text = selectedCommand.AnswerText };
                    return newMessage;
                }
            }
            newMessage = new Message() { Receiver = message.Sender, Sender = "Bot", Text = "The list of available commands can be found by sending: !help" };
            return newMessage;
        }

        private string FindCommand (string message)
        {
            if(message.Contains('!'))
            {
                var index = message.IndexOf('!');
                var commandSource = message.Substring(index).Split(new char[] {' ', '.' })[0];
                return commandSource;
            }
            else return "Empty";
        }

        public void AddMessage(IMessage message)
        {
            var session = _sessions.Find(x => x.ClientName == message.Sender);
            if(session != null)
            {
                session.AddMessage(message);
            }
        }

    }
}
