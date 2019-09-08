using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleChatBotLib 
{
    public interface IChatCommand
    {
        string Command { get; set; }
        string AnswerText { get; set; }
    }
}
