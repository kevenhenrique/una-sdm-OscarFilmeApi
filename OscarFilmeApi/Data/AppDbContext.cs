using Microsoft.EntityFrameworkCore;
using OscarFilmeApi.Models;

namespace OscarFilmeApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Filme> Filmes { get; set; }
    }
}
