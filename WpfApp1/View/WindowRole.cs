using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfApplDemo2018.ViewModel;
using WpfApplDemo2018.Model;
using System.Collections.ObjectModel;

namespace WpfApplDemo2018.Views
{
    public partial class WindowRole : Window
    {
        RoleViewModel vmRole = new RoleViewModel();
        Role tempRole;
        public WindowRole()
        {
            InitializeComponent();
            lvRole.ItemsSource = vmRole.ListRole;
            

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowNewRole wnRole = new WindowNewRole();
            // формирование кода новой должности
            int maxIdRole = vmRole.MaxId() + 1;
            Role role = new Role {
                Id = maxIdRole
            };
            wnRole.DataContext = role;
            if (wnRole.ShowDialog() == true)
            {
                vmRole.ListRole.Add(role);
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            WindowNewRole wnRole = new WindowNewRole
            {
                Title = "Редактирование должности",
                Owner = this
            };
            Role role = lvRole.SelectedItem as Role;
            if (role != null)
            {
                tempRole = role.ShallowCopy();
                wnRole.DataContext = tempRole;
            }
            if (wnRole.ShowDialog() == true)
            {
                // сохранение данных
                role.NameRole = tempRole.NameRole;
                lvRole.ItemsSource = null;
                lvRole.ItemsSource = vmRole.ListRole;
            } else
                {
                MessageBox.Show("Необходимо выбрать должность для редактированния",
                "Предупреждение",MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Role role = (Role)lvRole.SelectedItem;
            if (role != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные по должности: " +
                role.NameRole, "Предупреждение", MessageBoxButton.OKCancel,
                MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    vmRole.ListRole.Remove(role);
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать должность для удаления",
                "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}
