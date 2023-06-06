using Microsoft.EntityFrameworkCore;

namespace Gaas.GobbletGobblers.Application
{
    public class GameDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "GaneDb");
        }

        public DbSet<GameModel> Ganes { get; set; }
    }
}
