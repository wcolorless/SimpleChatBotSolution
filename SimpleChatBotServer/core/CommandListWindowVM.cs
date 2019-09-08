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
    public class CommandListWindowVM : INotifyPropertyChanged
    {
        private Command _AddNewCommandCommand;
        private Command _EditCommandCommand;
        private Command _RemoveCommandCommand;
        private Command _ExitAppCommand;
        private IChatCommand _SelectedChatCommand;

        private List<IChatCommand> _commands;

        public CommandListWindowVM(List<IChatCommand> commands)
        {
            _commands = commands;
        }

        public ObservableCollection<IChatCommand> Commands
        {
            get
            {
                ObservableCollection<IChatCommand> commands = new ObservableCollection<IChatCommand>();
                for (int i = 0; i < _commands.Count; i++) commands.Add(_commands[i]);
                return commands;
            }
        }

        public IChatCommand SelectedChatCommand
        {
            get { return _SelectedChatCommand; }
            set
            {
                _SelectedChatCommand = value;
                NotifyPropertyChanged("SelectedChatCommand");
            }
        }


        public Command AddNewCommandCommand
        {
            get
            {
                return _AddNewCommandCommand ?? (_AddNewCommandCommand = new Command(o =>
                {
                    _SelectedChatCommand = new ChatCommand() {Command = "!NewCommand", AnswerText = "NewAnswer" };
                    AddAndEditCommandWindow addAndEditCommandWindow = new AddAndEditCommandWindow(_SelectedChatCommand);
                    var result = addAndEditCommandWindow.ShowDialog();
                    if(result == true && (!string.IsNullOrEmpty(_SelectedChatCommand.Command) && !string.IsNullOrEmpty(_SelectedChatCommand.AnswerText)))
                    {
                        if (_commands != null)
                        {
                            _commands.Add(_SelectedChatCommand);
                            NotifyPropertyChanged("Commands");
                        }
                    }
                }));
            }
        }


        public Command EditCommandCommand
        {
            get
            {
                return _EditCommandCommand ?? (_EditCommandCommand = new Command(o =>
                {
                    if(_SelectedChatCommand != null)
                    {
                        AddAndEditCommandWindow addAndEditCommandWindow = new AddAndEditCommandWindow(_SelectedChatCommand);
                        var result = addAndEditCommandWindow.ShowDialog();
                    }
                }));
            }
        }


        public Command RemoveCommandCommand
        {
            get
            {
                return _RemoveCommandCommand ?? (_RemoveCommandCommand = new Command(o =>
                {
                    if (_SelectedChatCommand != null)
                    {
                        if (_commands != null)
                        {
                            _commands.Remove(_SelectedChatCommand);
                            NotifyPropertyChanged("Commands");
                        }
                    }
                }));
            }
        }


        public Command ExitAppCommand
        {
            get
            {
                return _ExitAppCommand ?? (_ExitAppCommand = new Command(o =>
                {
                    
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
