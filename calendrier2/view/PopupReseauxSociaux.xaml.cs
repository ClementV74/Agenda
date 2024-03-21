using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using calendrier2.contact_DB;
using calendrier2.service.DAO;
using Microsoft.EntityFrameworkCore;

namespace calendrier2.view
{
    public partial class PopupReseauxSociaux : Window
    {
        private List<ContactReseauxSociaux> _reseauxSociaux;
        public PopupReseauxSociaux(List<ContactReseauxSociaux> reseauxSociaux)
        {
            InitializeComponent();

            // Enregistrer la liste des réseaux sociaux
            _reseauxSociaux = reseauxSociaux.ToList();
            // Assigner les données de la DataGrid
            DataGridReseaux.ItemsSource = _reseauxSociaux;
        }

        private void Button_Retour_Click(object sender, RoutedEventArgs e)
        {
            var daoReseaux = new DAO_Reseaux(new ContactContext());
            daoReseaux.SaveChangesAndUpdateReseaux(_reseauxSociaux);

            // Fermer la fenêtre
            this.Close();
        }
    }
}
