using System.Collections.Generic;
using System.Windows;
using calendrier2.contact_DB;

namespace calendrier2.view
{
    public partial class PopupReseauxSociaux : Window
    {
        public PopupReseauxSociaux(List<ContactReseauxSociaux> reseauxSociaux)
        {
            InitializeComponent();

            // Assigner les données de la DataGrid
            DataGrid.ItemsSource = reseauxSociaux;
        }
    }
}
