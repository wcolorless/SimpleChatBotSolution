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
    public class ChatCommand : IChatCommand, INotifyPropertyChanged
    {

        private string _Command;
        private string _AnswerText;

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
            get { return _AnswerText; }
            set
            {
                _AnswerText = value;
                NotifyPropertyChanged("AnswerText");
            }
        }

        public ChatCommand()
        {

        }

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
