using Microsoft.AspNetCore.Mvc;
using PracticaExamenAWS.Models;
using PracticaExamenAWS.Repositories;

namespace PracticaExamenAWS.Controllers
{
    public class SeriesController : Controller
    {
        private RepositorySeries repository;

        public SeriesController(RepositorySeries repo)
        {
            this.repository = repo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.repository.GetSeriesAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await this.repository.FindSerieByIdAsync(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Serie serie)
        {
            await this.repository.CreateSerieAsync(serie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(await this.repository.FindSerieByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Serie serie)
        {
            await this.repository.UpdateSerieAsync(serie);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            await this.repository.DeleteSerie(id);
            return RedirectToAction("Index");
        }
    }
}
