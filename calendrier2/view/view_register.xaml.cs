using calendrier2.contact_DB;
using calendrier2.service.DAO;
using System;
using System.Windows;
using System.Windows.Controls;

namespace calendrier2.view
{
    public partial class view_register : UserControl
    {
        private DAO_Utilisateur _daoUtilisateur;

        public view_register(ContactContext dbContext)
        {
            InitializeComponent();
            _daoUtilisateur = new DAO_Utilisateur(dbContext);
        }


        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = TB_Username.Text;
            string password = TB_Password.Password;
            string confirmPassword = TB_ConfirmPassword.Password;

            // Vérifier si les mots de passe correspondent
            if (password != confirmPassword)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Ajouter l'utilisateur à la base de données en utilisant le DAO utilisateur
                _daoUtilisateur.AjouterUtilisateur(username, password);

                // Afficher un message de succès
                MessageBox.Show("Utilisateur ajouté avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Effacer les champs de saisie après l'ajout
                TB_Username.Text = "";
                TB_Password.Password = "";
                TB_ConfirmPassword.Password = "";
            }
            catch (Exception ex)
            {
                // Gérer les exceptions
                MessageBox.Show("Une erreur s'est produite lors de l'ajout de l'utilisateur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
