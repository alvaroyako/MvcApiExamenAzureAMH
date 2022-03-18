using Microsoft.AspNetCore.Mvc;
using MvcApiExamenAzureAMH.Models;
using MvcApiExamenAzureAMH.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiExamenAzureAMH.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceApiSeries service;

        public PersonajesController(ServiceApiSeries service)
        {
            this.service = service;
        }

        public IActionResult PersonajesCliente()
        {
            return View();
        }

        public async Task<IActionResult> PersonajesServidor()
        {
            List<Personaje> pjs = await this.service.GetPersonajesAsync();
            return View(pjs);
        }

        public IActionResult CreatePersonaje()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreatePersonaje(string nombre,string imagen,int idserie)
        {
            await this.service.InsertPersonajeAsync(nombre,imagen,idserie);
            return RedirectToAction("PersonajesServidor");
        }

        public async Task<IActionResult> DeletePersonaje(int idpj)
        {
            await this.service.DeletePersonajeAsync(idpj);
            return RedirectToAction("PersonajesServidor");
        }

        public async Task<IActionResult> UpdatePersonajeSerie()
        {
            List<Personaje> pjs = await this.service.GetPersonajesAsync();
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            return View(pjs);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePersonajeSerie(int idpersonaje,int idserie)
        {
            await this.service.UpdatePersonajeSerieAsync(idpersonaje, idserie);
            return RedirectToAction("PersonajesServidor");
        }
    }
}
