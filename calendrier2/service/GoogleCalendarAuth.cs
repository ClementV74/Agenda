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
        private static string[] Scopes = { CalendarService.Scope.Calendar };
        private static string ApplicationName = "calendrier";

        public static CalendarService GetCalendarService()
        {
            UserCredential credential;

            string credentialsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service", "credentials.json");

            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                string credPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "service", "token.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Créer le service Calendar à partir des informations d'authentification
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

    }

}
