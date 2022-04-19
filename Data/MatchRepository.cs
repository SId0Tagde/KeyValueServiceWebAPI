using assigntwo.Data.Entities;

namespace assigntwo.Data
{
    public class MatchRepository :IMatchRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<MatchRepository> logger;

        public MatchRepository(AppDbContext context, ILogger<MatchRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        //Add Match element
        public void Add(Match match) 
        {
            logger.LogInformation($"Adding an object of type {match.GetType()} to the context");
            context.Add(match);
        }

        //Delete Match element
        public void Delete(Match match){
            logger.LogInformation($"Removing the match by {match.Key} to the context");
            context.Remove(match);
        }

        //Whether Match exists by key
        public bool Exist(string key)
        {
            return  context.Matched.Where(x => x.Key == key.ToString()).Any();
         }

        //Getting Match by key
        public Match GetMatchbykey(string key)
        {
            logger.LogInformation($"Getting match by key");
            var query =  from contxt in context.Matched
                         where contxt.Key == key
                         select contxt;
            var a = query.ToArray();
            return a[0];
        }

        //For saving changes to repository
        public async Task<bool> SaveChanges()
        {
            logger.LogInformation($"Attempting to save changes in context");
            return (await context.SaveChangesAsync()) > 0;
        }

        //Update value for the specified key
        public void Update(string key, int value) 
        {
            var query = context.Matched.Where(m => m.Key == key).ToArray().ElementAt(0).Value = value;
        }
    }
}
