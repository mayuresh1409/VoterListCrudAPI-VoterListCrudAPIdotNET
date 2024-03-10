using Microsoft.EntityFrameworkCore;
using VoterListCrudAPI.Models;

namespace VoterListCrudAPI.Data
{
    public class VoterListCrudAPIDbContext : DbContext
    {
        public VoterListCrudAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Voter> Voters { get; set; }
    }
}
