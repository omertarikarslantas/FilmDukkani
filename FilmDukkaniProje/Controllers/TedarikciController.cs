using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmDukkaniProje.Models;

namespace FilmDukkaniProje.Controllers
{
    [Authorize]
    public class TedarikciController : Controller
    {
        // GET: Tedarikci
        FilmDukkaniEntities ctx = new FilmDukkaniEntities();
        public ActionResult Index()
        {
            List<Tedarikci> data = ctx.Tedarikci.ToList();
            return View(data);
        }
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tedarikci k)
        {
            ctx.Tedarikci.Add(k);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }






        [HttpPost]

        public string Sil (int id) 
        {
            Tedarikci t = ctx.Tedarikci.FirstOrDefault(x => x.TedarikciID == id);
            ctx.Tedarikci.Remove(t);
            try
            {
                ctx.SaveChanges();
                return "başarılı";
            }
            catch(Exception)
            {
                return "hata";
            }
            
        }
    }
}