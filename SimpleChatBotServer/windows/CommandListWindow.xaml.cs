using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SimpleChatBotLib;

namespace SimpleChatBotServer
{
    
    public partial class CommandListWindow : Window
    {
        List<IChatCommand> _commands;
        CommandListWindowVM commandListWindowVM;
        public CommandListWindow(List<IChatCommand> commands)
        {
            InitializeComponent();
            _commands = commands;
            DataContext = commandListWindowVM = new CommandListWindowVM(commands);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindowBtn(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
