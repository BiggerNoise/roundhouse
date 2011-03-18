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
using visual.roundhouse.ViewModels;

namespace visual.roundhouse.UserControls
{
    /// <summary>
    /// Interaction logic for OpenKicksTabControl.xaml
    /// </summary>
    public partial class OpenKicksTabControl
    {
        public OpenKicksTabControl()
        {
            InitializeComponent();
        }
        public void ShowKick(KickViewModel kick )
        {
            kicksTabControl.SelectedItem = kick;
        }

    }
}
