using System.Collections.Generic;
using System.Linq;
using calendrier2.contact_DB;

namespace calendrier2.service.DAO
{
    public class DAO_todolist
    {
        private readonly ContactContext _dbContext;

        public DAO_todolist(ContactContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Todolist> GetAllTodolists()
        {
            return _dbContext.Todolists.ToList();
        }

    }
}
