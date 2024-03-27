using calendrier2.contact_DB;
using System;
using System.Linq;
using System.Security.Cryptography; // Importer l'espace de noms System.Security.Cryptography pour utiliser MD5
using System.Text;

namespace calendrier2.service.DAO
{
    public class DAO_Utilisateur
    { 
        private ContactContext _dbContext; // Contexte de la base de données

        public DAO_Utilisateur(ContactContext dbContext) // Constructeur prenant le contexte de la base de données
        {
            _dbContext = dbContext; // Initialiser le contexte de la base de données
        }

        // Méthode pour vérifier les informations de connexion de l'utilisateur
        public bool VerifierIdentifiants(string nomUtilisateur, string motDePasse)
        {
            Utilisateur utilisateur = _dbContext.Utilisateurs.FirstOrDefault(u => u.Username == nomUtilisateur); // Rechercher l'utilisateur dans la base de données
            if (utilisateur == null)
                return false;

            return VerifyMd5Hash(motDePasse, utilisateur.Password); // Vérifier si le mot de passe correspond à son hash MD5
        }

        // Méthode pour ajouter un nouvel utilisateur à la base de données
        public void AjouterUtilisateur(string nomUtilisateur, string motDePasse) // Ajouter un nouvel utilisateur à la base de données
        {
            try
            {
                string motDePasseChiffre = ObtenirMd5Hash(motDePasse); // Chiffrer le mot de passe

                Utilisateur nouvelUtilisateur = new Utilisateur 
                {
                    Username = nomUtilisateur, // Stocker le nom d'utilisateur dans la base de données
                    Password = motDePasseChiffre // Stocker le mot de passe chiffré dans la base de données
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
                byte[] donnees = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(texte)); // Convertir le texte en tableau de bytes
                StringBuilder sBuilder = new StringBuilder(); // Créer un nouveau StringBuilder pour stocker le hash
                for (int i = 0; i < donnees.Length; i++) // Boucle pour convertir chaque byte en sa représentation hexadécimale
                {
                    sBuilder.Append(donnees[i].ToString("x2")); // Ajouter le byte au StringBuilder
                }
                return sBuilder.ToString(); // Retourner le hash MD5 sous forme de chaîne
            }
        }

        // Méthode pour vérifier si un mot de passe correspond à son hash MD5
        private bool VerifyMd5Hash(string motDePasse, string hash) // Vérifier si le mot de passe correspond à son hash MD5
        {
            string hashDuMotDePasse = ObtenirMd5Hash(motDePasse); // Obtenir le hash du mot de passe fourni
            StringComparer comparer = StringComparer.OrdinalIgnoreCase; // Créer un comparateur de chaînes pour comparer les deux hashs
            return comparer.Compare(hashDuMotDePasse, hash) == 0; // Comparer les deux hashs et retourner le résultat
        }

       
        public bool VerifyCredentials(string username, string password)
        {
            string hashedPassword = ObtenirMd5Hash(password); // Chiffrer le mot de passe
            Utilisateur user = _dbContext.Utilisateurs.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword); // Rechercher l'utilisateur dans la base de données
            return user != null; // Retourner vrai si l'utilisateur est trouvé, faux sinon
        }
    }
}
