using assigntwo.Data.Entities;

namespace assigntwo.Data
{
    public interface IMatchRepository
    {
        //Add Match element
        void Add(Match match) ;

        //Delete Match element
        void Delete(Match match);

        //Update value for the specified key
        void Update(string key, int value);

        //For saving changes to repository
        Task<bool> SaveChanges();

        //Whether Match exists by key
        public bool Exist(string key);

        //Getting Match by key
        Match GetMatchbykey(string key);

        
    }
}
