using calendrier2.service;
using calendrier2.contact_DB;
using calendrier2.view;
using calendrier2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace calendrier2.view
{
    /// <summary>
    /// Logique d'interaction pour view_contact.xaml
    /// </summary>
    public partial class view_contact : UserControl
    {
        public ObservableCollection<contact> Contacts { get; set; }

        public view_contact()
        {
            InitializeComponent();
            Contacts = new ObservableCollection<contact>();
            // Ajoutez quelques contacts pour tester
            Contacts.Add(new contact { Id = 1, Name = "Doe", Prenom = "John", Email = "[email protected]", Phone = "123-456-7890" });
            Contacts.Add(new contact { Id = 2, Name = "Smith", Prenom = "Anna", Email = "[email protected]", Phone = "098-765-4321" });

            // Lier le DataContext de votre UserControl à lui-même pour utiliser le Binding
            this.DataContext = this;
        }

        private void BTN_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new view_dashboard(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord

            // Remplace le contenu des deux parties de la grille
            Ecran_Contact.Children.Clear(); // Efface tout contenu existant dans la grille

            Grid.SetColumnSpan(dashboardView, 2);

            Ecran_Contact.Children.Add(dashboardView);
        }

        private void BTN_Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();


        }

        private void BTN_Calendrier_Click(object sender, RoutedEventArgs e)
        {
            var calendrierview = new view_calendrier(); // Assurez-vous de remplacer DashboardView par le nom de votre classe de vue du tableau de bord
            Ecran_Contact.Children.Clear(); // Efface tout contenu existant dans la grille
            Grid.SetColumnSpan(calendrierview, 2);
            Ecran_Contact.Children.Add(calendrierview);
        }
    }
}
