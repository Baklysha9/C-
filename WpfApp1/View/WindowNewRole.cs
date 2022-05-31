using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfApplDemo2018.Views
{
    public partial class WindowNewRole : Window
    {
        public WindowNewRole()
        {
            InitializeComponent();
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
