using Microsoft.AspNetCore.Mvc;
using MvcExamenPersonajesSeries.Models;
using MvcExamenPersonajesSeries.Services;

namespace MvcExamenPersonajesSeries.Controllers
{
    public class PersonajesController : Controller
    {
        private ServicePersonajes service;

        public PersonajesController(ServicePersonajes service)
        {
            this.service = service;
        } 

        public async Task<IActionResult> Personajes()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            List<string> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            return View(personajes);
        }

        [HttpPost]
        public async Task<IActionResult> Personajes(string serie)
        {
            List<Personaje> personajes = await this.service.GetPersonajesSeriesAsync(serie);
            List<string> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            return View(personajes);
        }

        public async Task<IActionResult> Details(int idpersonaje)
        {
            Personaje personaje = await this.service.GetPersonajeAsync(idpersonaje);
            return View(personaje);
        }

        public async Task<IActionResult> Delete(int idPersonaje)
        {
            await this.service.DeletePersonajeAsync(idPersonaje);
            return RedirectToAction("Personajes");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje pj)
        {
            await this.service.InsertPersonajeAsync(pj.IdPersonaje, pj.Nombre, pj.Imagen, pj.Serie);
            return RedirectToAction("Personajes");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Personaje personaje = await this.service.GetPersonajeAsync(id);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje pj)
        {
            await this.service.UpdatePersonajeAsync(pj.IdPersonaje, pj.Nombre, pj.Imagen, pj.Serie);
            return RedirectToAction("Index");
        }
    }
}
