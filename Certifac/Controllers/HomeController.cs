using Certifac.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.MobileControls;

namespace Certifac.Controllers
{
    public class HomeController : Controller
    {
        Cer_AddendasEntities db = new Cer_AddendasEntities();
        public ActionResult Index()
        {
           
            try
            {
                List<Addendas> ls = db.Addendas.ToList();
                db.Dispose();
                return View(ls);
            }
            catch (Exception ex)
            {
                TempData["e"] = ex.Message;
                return View();
            }
           
        }

        public ActionResult Create()
        {
            
                List<Addendas> ls = db.Addendas.ToList();
                db.Dispose();
                return View();
            
        }
        [HttpPost]
        public ActionResult Create(Addendas ad)
        {
            try
            {
                db.Addendas.Add(ad);
                db.SaveChanges();
                db.Dispose();

                return RedirectToAction("index");
            }
            catch 
            {

                return View();
            }
        }
        public ActionResult Edit(Guid id)
        {
            // Addendas ad = db.Addendas.Find(id);
            
           Addendas ad = db.Addendas.Where(x => x.IdAddenda == id).FirstOrDefault();
            return View(ad);
        }
        [HttpPost]
        public ActionResult Edit(Addendas ad)
        {
            try
            {
                Addendas adn = db.Addendas.Find(ad.IdAddenda);
                adn.NombreAddenda = ad.NombreAddenda;
                adn.XML = ad.XML;
                adn.FechaModificacion = ad.FechaModificacion;
                adn.Usuario = ad.Usuario;
                adn.Estado = ad.Estado;

                db.SaveChanges();
                db.Dispose();

                return RedirectToAction("index");

            }
            catch 
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult CambiarEstado(int id)
        {
            var ad = db.Addendas.Find(id);
            if (ad != null)
            {
                ad.Estado = !ad.Estado; // Invertir el estado actual
                db.SaveChanges();
                db.Dispose();
            }
            return RedirectToAction("Index"); // Redirigir de nuevo a la página de índice
        }
    }
}