using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace visual.roundhouse
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        Timer _duration;
        bool _sentToMain = false;
        public Splash()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _duration = new Timer(1000);
            _duration.AutoReset = false;
            _duration.Elapsed += new ElapsedEventHandler(_duration_Elapsed);
            _duration.Enabled = true;

        }

        void _duration_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(GoToMainWindow));
            _duration.Dispose();
        }
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            GoToMainWindow();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GoToMainWindow();
        }
        private void GoToMainWindow()
        {
            if (_sentToMain)
                return;

            Window mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
            _sentToMain = true;
        }

    }
}
