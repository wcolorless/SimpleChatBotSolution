using System;
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
using System.Reflection;
using System.Reflection.Emit;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace SimpleChatBotServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PrimeWindowVM primeWindowVM;
        public MainWindow()
        {
            InitializeComponent();
            ServerEnvironment.PrimeWindowDiapstcher = Application.Current.Dispatcher;
            DataContext = primeWindowVM = new PrimeWindowVM();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
