using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfApplDemo2018.Helper;
using WpfApplDemo2018.Model;
using WpfApplDemo2018.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfApplDemo2018.Views
{
    public partial class WindowEmployee : Window
    {
        private PersonViewModel vmPerson = MainWindow.vmPerson;
        private RoleViewModel vmRole;
        private ObservableCollection<PersonDPO> personsDPO;
        private List<Role> roles;

        public WindowEmployee()
        {
            InitializeComponent();
            vmPerson = new PersonViewModel();
            vmRole = new RoleViewModel();
            roles = vmRole.ListRole.ToList();
            // Формирование данных для отображения сотрудников с должностями
            // на базе коллекции класса ListPerson<Person>
            personsDPO = new ObservableCollection<PersonDPO>();
            foreach (var person in vmPerson.ListPerson)
            {
                PersonDPO p = new PersonDPO();
                p = p.CopyFromPerson(person);
                personsDPO.Add(p);
            }
            lvEmployee.ItemsSource = personsDPO;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowNewEmployee wnEmployee = new WindowNewEmployee
            {
                Title = "Новый сотрудник",
                Owner = this
            };
            // формирование кода нового собрудника
            int maxIdPerson = vmPerson.MaxId() + 1;
            PersonDPO per = new PersonDPO
            {
                Id = maxIdPerson,
                Birthday = DateTime.Now
            };
            wnEmployee.DataContext = per;
            wnEmployee.CbRole.ItemsSource = roles;
            if (wnEmployee.ShowDialog() == true)
            {
                Role r = (Role)wnEmployee.CbRole.SelectedValue;
                per.Role = r.NameRole;
                personsDPO.Add(per);
                // добавление нового сотрудника в коллекцию ListPerson<Person>
                Person p = new Person();
                p = p.CopyFromPersonDPO(per);
                vmPerson.ListPerson.Add(p);
            }

        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
 {
 WindowNewEmployee wnEmployee = new WindowNewEmployee
 {
 Title = "Редактирование данных",
 Owner = this
 };
 PersonDPO perDPO = (PersonDPO)lvEmployee.SelectedValue;
 PersonDPO tempPerDPO; // временный класс для редактирования
 if (perDPO != null)
 {
8
 tempPerDPO = perDPO.ShallowCopy();
 wnEmployee.DataContext = tempPerDPO;
 wnEmployee.CbRole.ItemsSource = roles;
 wnEmployee.CbRole.Text = tempPerDPO.Role;
 if (wnEmployee.ShowDialog() == true)
 {
 // перенос данных из временного класса в класс отображения данных
Role r = (Role)wnEmployee.CbRole.SelectedValue;
perDPO.Role = r.NameRole;
perDPO.FirstName = tempPerDPO.FirstName;
perDPO.LastName = tempPerDPO.LastName;
perDPO.Birthday = tempPerDPO.Birthday;
 lvEmployee.ItemsSource = null;
lvEmployee.ItemsSource = personsDPO;
 // перенос данных из класса отображения данных в класс Person
FindPerson finder = new FindPerson(perDPO.Id);
 List<Person> listPerson = vmPerson.ListPerson.ToList();
Person p = listPerson.Find(new Predicate<Person>(finder.PersonPredicate));
 p = p.CopyFromPersonDPO(perDPO);
 }
 }
 else
 {
 MessageBox.Show("Необходимо выбрать сотрудника для редактированния",
 "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
 }

 }



    }
}
