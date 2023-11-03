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
using WpfApp1.Data;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PersonDataApi context = new PersonDataApi();

            btnRef.Click += delegate { listView.ItemsSource = context.GetPeople().ToObservableCollection(); };
            btnAdd.Click += delegate
            {
                context.AddPerson(new Person()
                {
                    FirstName = txtFirstName.Text,
                    SecondName = txtSecondName.Text,
                    PaternalName = txtPaternalName.Text,
                    PhoneNumber = txtPhoneNumber.Text,
                    Address = txtAddress.Text,
                    Description = txtDescription.Text
                });
            };
        }
    }
}
