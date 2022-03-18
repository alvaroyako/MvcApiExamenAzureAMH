using Microsoft.AspNetCore.Mvc;
using MvcApiExamenAzureAMH.Models;
using MvcApiExamenAzureAMH.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiExamenAzureAMH.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceApiSeries service;

        public SeriesController(ServiceApiSeries service)
        {
            this.service = service;
        }

        public IActionResult SeriesCliente()
        {
            return View();
        }

        public async Task<IActionResult> SeriesServidor()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);           
        }

        public async Task<IActionResult> DetallesSerie(int idserie)
        {
            Serie serie = await this.service.FindSerieAsync(idserie);
            return View(serie);
        }

        public async Task <IActionResult> PersonajesSerie(int idserie)
        {
            List <Personaje> personajes= await this.service.FindPersonajesSerieAsync(idserie);
            return View(personajes);
        }

        public async Task <IActionResult> UpdateSerie(int idserie)
        {
            Serie serie = await this.service.FindSerieAsync(idserie);
            return View(serie);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSerie(Serie serie)
        {
            await this.service.UpdateSerieAsync(serie.IdSerie, serie.Nombre, serie.Imagen,serie.Puntuacion,serie.Año);
            return RedirectToAction("SeriesServidor");
        }


    }
}
