using calendrier2.contact_DB;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace calendrier2.service.DAO
{
    public class DAO_Utilisateur
    {
        private ContactContext _dbContext;

        public DAO_Utilisateur(ContactContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Méthode pour vérifier les informations de connexion de l'utilisateur
        public bool VerifierIdentifiants(string nomUtilisateur, string motDePasse)
        {
            Utilisateur utilisateur = _dbContext.Utilisateurs.FirstOrDefault(u => u.Username == nomUtilisateur);
            if (utilisateur == null)
                return false;

            return VerifyMd5Hash(motDePasse, utilisateur.Password);
        }

        // Méthode pour ajouter un nouvel utilisateur à la base de données
        public void AjouterUtilisateur(string nomUtilisateur, string motDePasse)
        {
            try
            {
                string motDePasseChiffre = ObtenirMd5Hash(motDePasse);

                Utilisateur nouvelUtilisateur = new Utilisateur
                {
                    Username = nomUtilisateur,
                    Password = motDePasseChiffre
                };

                _dbContext.Utilisateurs.Add(nouvelUtilisateur);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ajout de l'utilisateur : " + ex.Message);
                // Gérer l'erreur comme approprié
            }
        }

        // Méthode pour chiffrer le mot de passe en MD5
        private string ObtenirMd5Hash(string texte)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] donnees = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(texte));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < donnees.Length; i++)
                {
                    sBuilder.Append(donnees[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        // Méthode pour vérifier si un mot de passe correspond à son hash MD5
        private bool VerifyMd5Hash(string motDePasse, string hash)
        {
            string hashDuMotDePasse = ObtenirMd5Hash(motDePasse);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashDuMotDePasse, hash) == 0;
        }

       
        public bool VerifyCredentials(string username, string password)
        {
            string hashedPassword = ObtenirMd5Hash(password);
            Utilisateur user = _dbContext.Utilisateurs.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
            return user != null;
        }
    }
}
