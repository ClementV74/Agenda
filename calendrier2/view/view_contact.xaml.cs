using calendrier2.contact_DB;
using calendrier2.service.DAO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace calendrier2.view
{
    public partial class view_contact : UserControl
    {
        private DAO_contact _daoContact = new DAO_contact(); // Initialisez votre objet DAO_contact
        private List<string> _statusList;
        private Contact _selectedContact;
        private ContactContext _dbContext = new ContactContext();

        // Propriété pour la liste des contacts
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>(); // Créez une liste observable pour stocker les contacts


        public view_contact()
        {
            InitializeComponent();

            // Lier le DataContext de votre UserControl à lui-même pour utiliser le Binding
            this.DataContext = this;
            LoadStatusList();
            // Charger les contacts depuis la base de données
            Contacts = new ObservableCollection<Contact>(_dbContext.Contacts.ToList());

        }

        private void LoadStatusList()
        {
            _statusList = _daoContact.GetStatusList(); // Appel de la méthode dans votre DAO pour récupérer la liste des statuts
            StatusFilterListBox.ItemsSource = _statusList; // Liaison de la ListBox avec la liste des statuts
        }




        // Déclarez une liste pour stocker toutes les catégories sélectionnées
        private void StatusFilterListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Effacez les contacts actuellement affichés
            Contacts.Clear();

            // Vérifiez si l'option "Tous" est sélectionnée
            if (StatusFilterListBox.SelectedItems.Contains("Tous"))
            {
                // Si "Tous" est sélectionné, affichez tous les contacts
                foreach (var contact in _dbContext.Contacts)
                {
                    Contacts.Add(contact);
                }
            }
            else
            {
                // Parcourez toutes les catégories sélectionnées
                foreach (string selectedCategory in StatusFilterListBox.SelectedItems)
                {
                    // Filtrer les contacts en fonction de la catégorie sélectionnée
                    List<Contact> filteredContacts = _dbContext.Contacts.Where(c => c.Status == selectedCategory).ToList();

                    // Ajouter les contacts filtrés à la liste des contacts
                    foreach (var contact in filteredContacts)
                    {
                        Contacts.Add(contact);
                    }
                }
            }
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
                MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer ce contact ?", "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _daoContact.DeleteContact(contactASupprimer.IdContact);
                    Contacts.Remove(contactASupprimer);
                }
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
                // Récupérer le contact à modifier depuis la base de données
                Contact contactModifie = _dbContext.Contacts.FirstOrDefault(c => c.IdContact == contactAModifier.IdContact);

                if (contactModifie != null)
                {
                    // Mettez à jour les propriétés du contact avec les valeurs de la ligne sélectionnée
                    contactModifie.Nom = contactAModifier.Nom;
                    contactModifie.Prenom = contactAModifier.Prenom;
                    contactModifie.Email = contactAModifier.Email;
                    contactModifie.Tel = contactAModifier.Tel;



                    // Rafraîchir la liste des contacts après la mise à jour
                    RefreshContacts();
                }
                else
                {
                    MessageBox.Show("Le contact sélectionné n'existe pas dans la base de données.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }



        // Méthode pour rafraîchir la liste des contacts
        private void RefreshContacts()
        {
            Contacts.Clear();
            foreach (var contact in _dbContext.Contacts.ToList())
            {
                Contacts.Add(contact);
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
                    var contact = o as Contact;
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
