﻿using calendrier2.contact_DB;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace calendrier2.view
{
    public partial class view_contact : UserControl
    {

        private Contact _selectedContact;
        private ContactContext _dbContext = new ContactContext();

        // Propriété pour la liste des contacts
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();

        public view_contact()
        {
            InitializeComponent();

            // Lier le DataContext de votre UserControl à lui-même pour utiliser le Binding
            this.DataContext = this;

            // Charger les contacts depuis la base de données
            Contacts = new ObservableCollection<Contact>(_dbContext.Contacts.ToList());
        }

       

        private void BTN_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new view_dashboard();
            Ecran_Contact.Children.Clear();
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
            var calendrierview = new view_calendrier();
            Ecran_Contact.Children.Clear();
            Grid.SetColumnSpan(calendrierview, 2);
            Ecran_Contact.Children.Add(calendrierview);
        }

        private void BTN_AddContact_Click(object sender, RoutedEventArgs e)
        {

            var addcontactview = new view_addcontact();
            Ecran_Contact.Children.Clear();
            Grid.SetColumnSpan(addcontactview, 2);
            Ecran_Contact.Children.Add(addcontactview);
        }

        private void BTN_Supp_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridContacts.SelectedItem is Contact contactASupprimer)
            {
                // Supprimer le contact de la base de données
                _dbContext.Contacts.Remove(contactASupprimer);
                _dbContext.SaveChanges();

                // Supprimer le contact de la liste des contacts liée à l'interface utilisateur
                Contacts.Remove(contactASupprimer);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un contact à supprimer.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BTN_Modifier_CLick(object sender, RoutedEventArgs e)
        {
            if (DataGridContacts.SelectedItem is Contact contactAModifier)
            {
                // Obtenez l'index de la ligne sélectionnée dans le DataGrid
                int selectedIndex = DataGridContacts.SelectedIndex;

                // Mettez à jour les propriétés du contact avec les valeurs de la ligne sélectionnée
                Contact contactModifie = Contacts[selectedIndex];
                contactModifie.Nom = contactAModifier.Nom;
                contactModifie.Prenom = contactAModifier.Prenom;
                contactModifie.Email = contactAModifier.Email;
                contactModifie.Tel = contactAModifier.Tel;

                // Enregistrez les modifications dans la base de données
                _dbContext.SaveChanges();

            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un contact à modifier.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

       

        

        private void TB_Search(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower(); // Convertissez le texte en minuscules pour une comparaison insensible à la casse
            ICollectionView cv = CollectionViewSource.GetDefaultView(DataGridContacts.ItemsSource);
            if (string.IsNullOrEmpty(searchText))
            {
                cv.Filter = null;
            }
            else
            {
                cv.Filter = o =>
                {
                    // Vérifiez si le texte de recherche correspond à l'un des champs dans la ligne de la DataGrid
                    var contact = o as Contact; // Remplacez YourContactClass par la classe réelle de vos contacts
                    return contact.Nom.ToLower().Contains(searchText) ||
                           contact.Prenom.ToLower().Contains(searchText) ||
                           contact.Email.ToLower().Contains(searchText) ||
                           contact.Tel.ToLower().Contains(searchText);
                };
            }
        }

        private void BTN_Details_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridContacts.SelectedItem is Contact selectedContact)
            {
                var detailview = new view_DetailUser(selectedContact);
                Ecran_Contact.Children.Clear();
                Grid.SetColumnSpan(detailview, 2);
                Ecran_Contact.Children.Add(detailview);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un contact pour afficher les détails.", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



    }
}