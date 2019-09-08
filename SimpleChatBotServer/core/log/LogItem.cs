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

namespace SimpleChatBotServer
{
    public class LogItem : INotifyPropertyChanged
    {
        private string _Message;
        private DateTime _DateTime;

        public string Message
        {
            get { return _Message; }
            set
            {
                _Message = value;
                NotifyPropertyChanged("Message");
            }
        }


        public DateTime DateTime
        {
            get { return _DateTime; }
            set
            {
                _DateTime = value;
                NotifyPropertyChanged("DateTime");
            }
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
