using ExpenseTrackerAPI.Database;

namespace ExpenseTrackerAPI.Repo
{
    public class TransactionsRepo
    {
        private readonly DatabaseContext _dataContext;

        public TransactionsRepo(DatabaseContext dataContext)
        {
            _dataContext = dataContext;
        }

    }
}
