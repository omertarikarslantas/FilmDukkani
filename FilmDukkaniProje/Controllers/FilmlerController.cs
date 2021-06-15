using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmDukkaniProje.Models;
namespace FilmDukkaniProje.Controllers
{
    public class FilmlerController : Controller
    {

        // GET: Filmler
        FilmDukkaniEntities ctx = new FilmDukkaniEntities();
        public ActionResult Index()
        {
            List<Filmler> filmlers = ctx.Filmler.ToList();
            return View(filmlers);
        }


        //GET: Film Ekle
        public ActionResult FilmiListeyeEkle(int Id)
        {
            var film = ctx.Liste.FirstOrDefault(c => c.FilmID == Id && c.KullaniciUsername == User.Identity.Name);
            if (film == null)
            {
                ctx.Liste.Add(new Liste()
                {
                    Adet = 1,
                    EklenmeTarihi = DateTime.Now,
                    FilmID = Id,
                    KullaniciUsername = User.Identity.Name
                });
                ctx.SaveChanges();

                Session["FilmListeyeEkle"] = "Film Listeye Eklenmiştir.";
            }

            return Redirect("/Filmler/Index");
        }
        
        //GET: Film Ekle
        public ActionResult FilmiListedenKaldir(int Id)
        {
            var filmler = ctx.Liste.Where(c => c.FilmID == Id && c.KullaniciUsername == User.Identity.Name).ToList();
            ctx.Liste.RemoveRange(filmler);
            ctx.SaveChanges();

            Session["FilmiListedenKaldir"] = "Film Listeden Kaldirilmiştir.";

            return Redirect("/Kullanici/Sepetim");
        }

        public ActionResult Onayla()
        {
            var filmler = ctx.Liste.Where(c => c.KullaniciUsername == User.Identity.Name && c.KiralamaTarihi == null).ToList();

            foreach (var item in filmler)
            {
                var film = ctx.Filmler.FirstOrDefault(c => c.FilmID == item.FilmID && c.Stok > 0);
                if (film != null)
                {
                    film.Stok--;
                    item.KiralamaTarihi = DateTime.Now;
                }

            }
            ctx.SaveChanges();

            Session["Onayla"] = "Filmler onaylanmıştır.";

            return Redirect("/Kullanici/Sepetim");
        }
    }
}