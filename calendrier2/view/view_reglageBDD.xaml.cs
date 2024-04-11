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
using calendrier2.service;

namespace calendrier2.view
{
    /// <summary>
    /// Logique d'interaction pour view_reglageBDD.xaml
    /// </summary>
    public partial class view_reglageBDD : UserControl
    {
        public view_reglageBDD()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string host = TB_Host.Text;
            string user = TB_User.Text;
            string password = TB_Password.Text;
            string database = TB_Database.Text;

            // Vous pouvez également valider les entrées utilisateur ici

            BDD_modifier bddModifier = new BDD_modifier();
            bddModifier.ModifierConnexionBDD(host, "port", user, password, database);
        }
    }
}

