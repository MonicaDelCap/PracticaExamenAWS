using Microsoft.EntityFrameworkCore;
using PracticaExamenAWS.Data;
using PracticaExamenAWS.Models;

namespace PracticaExamenAWS.Repositories
{
    public class RepositorySeries
    {
        private SeriesContext context;
        public RepositorySeries(SeriesContext con)
        {
            this.context = con;
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            return await this.context.Series.ToListAsync();
        }

        public async Task<Serie> FindSerieByIdAsync(int id)
        {
            return await this.context.Series.Where(x => x.IdSerie == id).FirstOrDefaultAsync();
        }

        public async Task<int> GetMaxSerieId()
        {
            return await this.context.Series.MaxAsync(x => x.IdSerie);
        }

        public async Task CreateSerieAsync(Serie serie)
        {
            Serie s = new Serie();
            s.IdSerie = await this.GetMaxSerieId() + 1;
            s.Nombre = serie.Nombre;
            s.Imagen = serie.Imagen;
            s.Anio = serie.Anio;

            await this.context.Series.AddAsync(s);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateSerieAsync(Serie serie)
        {
            Serie s = await FindSerieByIdAsync(serie.IdSerie);
            s.Nombre = serie.Nombre;
            s.Imagen = serie.Imagen;
            s.Anio = serie.Anio;
            
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteSerie(int id)
        {
            this.context.Series.Remove(await FindSerieByIdAsync(id));
            await this.context.SaveChangesAsync();
        }
    }
}
