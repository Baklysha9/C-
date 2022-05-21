using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfApplDemo2018.ViewModel;

namespace WpfApplDemo2018.Views
{
    public partial class WindowRole : Window
    {
        public WindowRole()
        {
            InitializeComponent();

            RoleViewModel vmRole = new RoleViewModel();
            lvRole.ItemsSource = vmRole.ListRole;

        }
    }
}
