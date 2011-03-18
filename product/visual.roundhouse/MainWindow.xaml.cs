using System;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;
using visual.roundhouse.Models;
using visual.roundhouse.ViewModels;

namespace visual.roundhouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            OpenKicks = new ObservableCollection<KickViewModel>();
        }

        private void FileOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "db",
                DefaultExt = ".xml",
                Filter = "Roundhouse KickFiles (.kick)|*.kick",
                Title = "Select a Kick to Open",
                CheckFileExists = true,
                ValidateNames = true
            };
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                foreach(string name in dlg.SafeFileNames)
                    OpenKickFile(name);
            }
        }
        private void OpenKickFile(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            if (!file.Exists)
                return;
            KickViewModel kvm = new KickViewModel(new KickModel(file));
            OpenKicks.Add( kvm );
            openKicksUi.ShowKick(kvm);
        }

        public ObservableCollection<KickViewModel> OpenKicks { get; private set; }
    }
}
