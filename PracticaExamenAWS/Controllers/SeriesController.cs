using Microsoft.AspNetCore.Mvc;
using PracticaExamenAWS.Models;
using PracticaExamenAWS.Repositories;
using PracticaExamenAWS.Services;

namespace PracticaExamenAWS.Controllers
{
    public class SeriesController : Controller
    {
        private RepositorySeries repository;
        private ServiceBucketS3 service;
        public SeriesController(RepositorySeries repo, ServiceBucketS3 ser)
        {
            this.repository = repo;
            this.service = ser;
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
        public async Task<IActionResult> Create(Serie serie, IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            {
                await this.service.UploadFileAsync(file.FileName, stream);
            }
            serie.Imagen = file.FileName;
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
