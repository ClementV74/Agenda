using calendrier2.contact_DB;
using System.Collections.Generic;
using System.Linq;

namespace calendrier2.service.DAO
{
    class DAO_tache
    {
        // Méthode pour récupérer la liste des tâches depuis la base de données
        public List<Tache> GetTaskList()
        {
            using (var dbContext = new ContactContext()) // Utilisation du contexte de base de données ContactContext
            {
                return dbContext.Taches.ToList(); // Récupération de la liste des tâches
            }
        }
    }
}
