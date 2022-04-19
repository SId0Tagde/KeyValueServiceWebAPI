using assigntwo.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace assigntwo.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Match> Matched { get; set; } = null!;


    }

}
