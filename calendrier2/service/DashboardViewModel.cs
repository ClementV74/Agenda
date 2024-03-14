using calendrier2.contact_DB;
using calendrier2.service.DAO;
using System.Collections.ObjectModel;
using System.Linq;

namespace calendrier2.view
{
    public class DashboardViewModel
    {
        public ObservableCollection<Tache> Taches { get; set; }

        public DashboardViewModel()
        {
            // Initialisez la liste des tâches en utilisant DAO_tache
            DAO_tache daoTache = new DAO_tache();
            Taches = new ObservableCollection<Tache>(daoTache.GetTaskList());
        }
    }
}
