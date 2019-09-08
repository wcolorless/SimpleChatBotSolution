using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
using System.Threading.Tasks;
using SimpleChatBotLib;
using WCOLORLESS.Hiny;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Timers;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Threading;


namespace SimpleChatBotClient
{
    public class PrimeWindowVM 
    {
        private Command _ConnectToServerCommand;
        private Command _SendMessageToChatBotCommand;
        private Command _ClearAllMessagesCommand;
        private Command _DisconnectFromServerCommand;
        private Command _ExitAppCommand;
        private string _LoginName = "ClientName";
        private bool _IsConnected;
        //
        private ChatBotCredentials _chatBotCredentials;
        private string _CurrentMessage;
        //
        private ObservableCollection<Message> _chatMessages = new ObservableCollection<Message>();
        //
        private TextBox _messageBox;


        public string LoginName
        {
            get { return _LoginName; }
            set
            {
                _LoginName = value;
            }
        }

        public TextBox ChatMessageBox
        {
            get { return _messageBox; }
            set
            {
                _messageBox = value;
            }
        }

        public string CurrentMessage
        {
            get { return _CurrentMessage; }
            set
            {
                _CurrentMessage = value;
                NotifyPropertyChanged("CurrentMessage");
            }
        }

        public ObservableCollection<Message> ChatMessages
        {
            get { return _chatMessages; }
            set
            {
                _chatMessages = value;
                NotifyPropertyChanged("ChatMessages");
            }

        }


        private HinyClient hinyClient;

        public PrimeWindowVM()
        {
            _chatBotCredentials = new ChatBotCredentials() { Name = _LoginName };
            hinyClient = new HinyClient("http://localhost:11000/HinyServer/");
        }


        public Command ConnectToServerCommand
        {
            get
            {
                return _ConnectToServerCommand ?? (_ConnectToServerCommand = new Command(o =>
                {
                    if (!string.IsNullOrEmpty(_LoginName)) SendLogin();
                    else MessageBox.Show("Chat Name is Empty");
                }));
            }
        }

        private async void SendLogin()
        {
            if (!_IsConnected)
            {
                _chatBotCredentials = new ChatBotCredentials() { Name = _LoginName };
                HinyTransportBaseObject loginRequest = new HinyTransportBaseObject("Login") { Request = _chatBotCredentials };
                var operation = await hinyClient.Request<HinyTransportBaseObject>(loginRequest);
                var loginResponce = operation.Response;
                if (!((Message)loginResponce).Text.Contains("Your name already exist"))
                {
                    _IsConnected = true;
                }
                else _IsConnected = false;
                _chatMessages.Add((Message)loginResponce);
                _CurrentMessage = string.Empty;
                NotifyPropertyChanged("CurrentMessage");
                ChatMessageBox.Clear();
            }
            else MessageBox.Show("You are already logged ");
        }


        public Command SendMessageToChatBotCommand
        {
            get
            {
                return _SendMessageToChatBotCommand ?? (_SendMessageToChatBotCommand = new Command(o =>
                {
                    if(!string.IsNullOrEmpty(_CurrentMessage))
                    {
                        SendMessage();
                    }
                }));
            }
        }

        private async void SendMessage()
        {
            var newMessage = new Message() { Sender = _chatBotCredentials.Name, Receiver = "", Text = _CurrentMessage };
            _chatMessages.Add(newMessage);
            HinyTransportBaseObject MessageRequest = new HinyTransportBaseObject("SendMessage") { Request = newMessage };
            var operation = await hinyClient.Request<HinyTransportBaseObject>(MessageRequest);
            _chatMessages.Add((Message)operation.Response);
            _CurrentMessage = string.Empty;
            NotifyPropertyChanged("CurrentMessage");
            ChatMessageBox.Clear();
        }

        public Command ClearAllMessagesCommand
        {
            get
            {
                return _ClearAllMessagesCommand ?? (_ClearAllMessagesCommand = new Command(o =>
                {
                    _chatMessages.Clear();
                }));
            }
        }


        public Command DisconnectFromServerCommand
        {
            get
            {
                return _DisconnectFromServerCommand ?? (_DisconnectFromServerCommand = new Command(o =>
                {
                    SendLogout();
                }));
            }
        }
        private async void SendLogout()
        {
            if (_IsConnected)
            {
                HinyTransportBaseObject LogoutRequest = new HinyTransportBaseObject("Logout") { Request = _chatBotCredentials };
                var operation = await hinyClient.Request<HinyTransportBaseObject>(LogoutRequest);
                var logoutResponce = operation.Response;
                if ((bool)logoutResponce)
                {
                    _IsConnected = false;
                }
                else _IsConnected = true;
            }
            else MessageBox.Show("You are not logged");

        }


        public Command ExitAppCommand
        {
            get
            {
                return _ExitAppCommand ?? (_ExitAppCommand = new Command(o =>
                {
                    SendLogout();
                    Application.Current.Shutdown();
                }));
            }
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
