using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleChatBotLib
{
    public interface IMessage
    {
        string Sender { get; set; }
        string Receiver { get; set; }
        string Text { get; set; }
    }
}
