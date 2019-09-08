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
using SimpleChatBotLib;
using WCOLORLESS.Hiny;

namespace SimpleChatBotLib 
{
    [JsonObject]
    public class Message : IMessage,  INotifyPropertyChanged
    {
        private string _Sender;
        private string _Receiver;
        private string _Text;


        public string Sender
        {
            get { return _Sender; }
            set
            {
                _Sender = value;
            }
        }
        public string Receiver
        {
            get { return _Receiver; }
            set
            {
                _Receiver = value;
            }
        }

        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
            }
        }

        [field:NonSerialized]
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
