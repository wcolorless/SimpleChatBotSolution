using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Timers;
using Newtonsoft.Json;

namespace SimpleChatBotLib
{
    public class HelpChatCommand : IChatCommand, INotifyPropertyChanged
    {
        private string _Command = "!help";
        private string _AnswerText;
        private List<IChatCommand> _AllCommands;

        public string Command
        {
            get { return _Command; }
            set
            {
                _Command = value;
                NotifyPropertyChanged("Command");
            }
        }
        public string AnswerText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Commands List:\n");
                for (int i = 0; i < _AllCommands.Count; i++)
                {
                   if(_AllCommands[i].Command != _Command && _AllCommands[i].Command != "!help")  sb.Append(_AllCommands[i].Command + "\n");
                }
                _AnswerText = sb.ToString();
                return _AnswerText;
            }
            set
            {
                _AnswerText = value;
                NotifyPropertyChanged("AnswerText");
            }
        }

        public HelpChatCommand(List<IChatCommand> AllCommands)
        {
            _AllCommands = AllCommands;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
