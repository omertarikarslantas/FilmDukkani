using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmDukkaniProje.Models;
namespace FilmDukkaniProje.Controllers
{
    [Authorize]
    
    public class FilmController : Controller
    {
        // GET: Film
        FilmDukkaniEntities ctx = new FilmDukkaniEntities();
        public ActionResult Index()
        {
            List<Filmler> filmler = ctx.Filmler.ToList();
            return View(filmler);
        }
        public ActionResult FilmEkle()
        {
            ViewBag.Kategoriler = ctx.Kategori.ToList();
            ViewBag.Tedarikciler = ctx.Tedarikci.ToList();
            ViewBag.AltYazilar = ctx.AltYazi.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult FilmEkle(Filmler u)
        {
            if(Request.Files.Count>0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                u.Gorsel = "/Image/" + dosyaadi + uzanti;
            }

            ctx.Filmler.Add(u);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            Filmler u = ctx.Filmler.FirstOrDefault(x => x.FilmID == id);
            return View(u);
        }
        [HttpPost]
        public ActionResult Sil(Filmler u)
        {
            Filmler fil = ctx.Filmler.FirstOrDefault(x => x.FilmID == u.FilmID);
            ctx.Filmler.Remove(fil);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(Filmler p)
        {
            var ara = ctx.Filmler.Find(p.FilmID);
            return View("Guncelle", ara);
        }
        [HttpPost]
        public ActionResult Guncel(Filmler u)
        {
            var guncelle = ctx.Filmler.Find(u.FilmID);
            guncelle.FilmID = u.FilmID;
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

