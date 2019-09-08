﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleChatBotLib;
using WCOLORLESS.Hiny;

namespace SimpleChatBotClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PrimeWindowVM primeWindowVM;
        public  MainWindow()
        {
            InitializeComponent();
            primeWindowVM = new PrimeWindowVM();
            primeWindowVM.ChatMessageBox = MessageBox; // Binding not working :)
            DataContext = primeWindowVM;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
