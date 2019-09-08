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
using System.Windows.Controls;
using SimpleChatBotLib;
using WCOLORLESS.Hiny;
using System.Windows.Threading;


namespace SimpleChatBotServer
{
    public class PrimeWindowVM : INotifyPropertyChanged
    {
        private Command _OpenCommandListWindowCommand;
        private Command _SaveLogFileToCSVCommand;
        private Command _ExitAppCommand;
        private HinyServer<HinyTransportBaseObject, Service> server;
        private HinyHttpResponseProvider<HinyTransportBaseObject, Service> httpResponseProvider;
        private Service service;
        private Logger logger;
        // 
        private ObservableCollection<ChatBotCredentials> _credentials = new ObservableCollection<ChatBotCredentials>();
        //

        public ObservableCollection<ChatBotCredentials> Credentials
        {
            get { return _credentials; }
            set
            {
                _credentials = value;
                NotifyPropertyChanged("Credentials");
            }
        }

        public ObservableCollection<LogItem> LogItems
        {
            get { return logger.Items; }
        }


        private bool UpdateCredentials(ChatBotCredentials obj, CredentialsOpTypes type)
        {
            bool result = false;
            ServerEnvironment.PrimeWindowDiapstcher.Invoke(() => {
                if(type == CredentialsOpTypes.Add)
                {
                    var rObj = _credentials.ToList().Find(x => x.Name == obj.Name);
                    if (rObj == null)
                    {
                        _credentials.Add(obj);
                        result = true;
                    }
                }
                else if(type == CredentialsOpTypes.Remove)
                {
                    var rObj = _credentials.ToList().Find(x => x.Name == obj.Name);
                    if(rObj != null)
                    {
                        _credentials.Remove(rObj);
                        result = true;
                    }
                }
            });
            return result;
        }


        public PrimeWindowVM()
        {
            logger = Logger.GetInstance();
            service = new Service();
            service.UpdateCredentialsList = UpdateCredentials;
            httpResponseProvider = new HinyHttpResponseProvider<HinyTransportBaseObject, Service>(service);
            server = new HinyServer<HinyTransportBaseObject, Service>("http://localhost:11000/HinyServer/", httpResponseProvider);
            server.Start();
            logger.AddItem("Application is Run");
        }


        public Command ExitAppCommand
        {
            get
            {
                return _ExitAppCommand ?? (_ExitAppCommand = new Command(o =>
                {
                    Application.Current.Shutdown();
                }));
            }
        }

        public Command OpenCommandListWindowCommand
        {
            get
            {
                return _OpenCommandListWindowCommand ?? (_OpenCommandListWindowCommand = new Command(o =>
                {
                    CommandListWindow commandListWindow = new CommandListWindow(service.Commands);
                    var result = commandListWindow.ShowDialog();

                }));
            }
        }

        public Command SaveLogFileToCSVCommand
        {
            get
            {
                return _SaveLogFileToCSVCommand ?? (_SaveLogFileToCSVCommand = new Command(o =>
                {
                    logger.SaveLogs();
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
