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
    /// <summary>
    /// Логика взаимодействия для AddAndEditCommandWindow.xaml
    /// </summary>
    public partial class AddAndEditCommandWindow : Window
    {
        public AddAndEditCommandWindow(IChatCommand command)
        {
            InitializeComponent();
            DataContext = command;
        }

        private void SaveBtn(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CloseWindowBtn(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
