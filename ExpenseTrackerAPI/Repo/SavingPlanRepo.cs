using ExpenseTrackerAPI.Database;

namespace ExpenseTrackerAPI.Repo
{
    public class SavingPlanRepo
    {
        private readonly DatabaseContext _dbcontext;

        public SavingPlanRepo(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //public async Task<int> CreateSavingPlanAsync() { }

        //public async Task<int> DeleteSavingPlanAsync() { }

        //public async Task<int> UpdateSavingPlanAsync() { }
    }
}
