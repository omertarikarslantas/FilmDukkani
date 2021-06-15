using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmDukkaniProje.Models;

namespace FilmDukkaniProje.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        // GET: Kategori
        FilmDukkaniEntities ctx = new FilmDukkaniEntities();
        public ActionResult Index()
        {
            List<Kategori> ktg = ctx.Kategori.ToList();
            return View(ktg);
        }

        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public  ActionResult Ekle(Kategori k)
        {
            ctx.Kategori.Add(k);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public void Sil (int id)
        {
            Kategori k = ctx.Kategori.FirstOrDefault(x => x.KategoriID == id);
            ctx.Kategori.Remove(k);
            ctx.SaveChanges();
        }
    }
}