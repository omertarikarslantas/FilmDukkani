using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmDukkaniProje.Models;
namespace FilmDukkaniProje.Controllers
{
    [Authorize]
    public class PaketController : Controller
    {
        // GET: Paket
        FilmDukkaniEntities ctx = new FilmDukkaniEntities();
        public ActionResult Index()
        {
            List<Paket> pkt = ctx.Paket.ToList();
            return View(pkt);
        }
        public ActionResult Ekle()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Ekle(Paket p)
        {
            ctx.Paket.Add(p);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public void Sil(int id) 
        {
            Paket p = ctx.Paket.FirstOrDefault(x => x.PaketID == id);
            ctx.Paket.Remove(p);
            ctx.SaveChanges();
        }
    }
}