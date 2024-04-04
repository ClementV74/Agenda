using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace calendrier2.service
{
    public class GoogleCalendarAuth
    {
        private static string[] Scopes = { CalendarService.Scope.Calendar }; // Autorisations requises pour accéder au calendrier
        private static string ApplicationName = "calendrier"; // Nom de l'application

        public static CalendarService GetCalendarService()  
        {
            UserCredential credential; // Créer un objet UserCredential pour stocker les informations d'authentification

            string credentialsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service", "credentials.json"); // Chemin d'accès au fichier de clés
             
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))  // Ouvrir le fichier de clés
            {
                string credPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service", "token.json");     // Chemin d'accès au fichier de jetons

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync( // Autoriser l'accès au calendrier
                    GoogleClientSecrets.FromStream(stream).Secrets, // Charger les informations d'identification du client
                    Scopes, // Autorisations requises
                    "user", // Identifiant de l'utilisateur
                    CancellationToken.None, // Annulation
                    new FileDataStore(credPath, true)).Result;  // Stockage des jetons d'accès
            }

            // Créer le service Calendar à partir des informations d'authentification
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential, // Informations d'authentification
                ApplicationName = ApplicationName, // Nom de l'application
            });

            return service;
        }

     

    }

}
