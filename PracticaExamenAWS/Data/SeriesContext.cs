using Microsoft.EntityFrameworkCore;
using PracticaExamenAWS.Models;

namespace PracticaExamenAWS.Data
{
    public class SeriesContext:DbContext
    {
        public SeriesContext(DbContextOptions<SeriesContext> options):base(options) { }

        public DbSet<Serie> Series { get; set; }
    }
}
