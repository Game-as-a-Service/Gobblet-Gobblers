using Microsoft.EntityFrameworkCore;

namespace Wsa.Gaas.GobbletGobblers.Application
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
