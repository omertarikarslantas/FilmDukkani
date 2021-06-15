using FilmDukkaniProje.App_Classes;
using FilmDukkaniProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FilmDukkaniProje.Controllers
{
    [Authorize]
    
    public class KullaniciController : Controller
    {
        FilmDukkaniEntities ctx = new FilmDukkaniEntities();

        // GET: Kullanici
        public ActionResult Index()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);
        }
        [AllowAnonymous]
        public ActionResult Ekle()
        {
            return View();

        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Ekle(Kullanici k)
        {
            MembershipCreateStatus durum;
            Membership.CreateUser(k.KullaniciAdi, k.Parola, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);

            string mesaj = "";
            switch (durum)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += " Geçersiz Kullanıcı Adı Girildi.";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += " Geçersiz Parola Girildi.";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += " Geçersiz Gizli Soru Girildi.";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += " Kullanılmış  Gizli Cevap Girildi.";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += " Kullanılmış Kullanıcı Adı Girildi.";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += " Kullanılmış Mail Adresi Girildi.";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += " Kullanıcı Engel Hatası.";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj += " Geçersiz Kullanıcı Key Girildi.";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += " Kullanılmış kullanıcı key hatası.";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj += " Üst Yönetimi sağlayıcısı hatası.";
                    break;
                default:
                    break;
            }
            ViewBag.Mesaj = mesaj;
            if (durum == MembershipCreateStatus.Success)
                return RedirectToAction("Index");
            else
                return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult RolAta() 
        {
            ViewBag.Roller = Roles.GetAllRoles().ToList();
            ViewBag.Kullanicilar = Membership.GetAllUsers();
            return View();
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]

        public ActionResult RolAta(string KullaniciAdi,string RolAdi)
        {
            Roles.AddUserToRole(KullaniciAdi, RolAdi);
            return RedirectToAction("Index");
        }

        [HttpPost]

        public string UyeRolleri(string kullaniciAdi)
        {
           List<string> roller=Roles.GetRolesForUser(kullaniciAdi).ToList();
            string rol = "";
            foreach (string r in roller)
            {
                rol += r + "-";
            }
            rol = rol.Remove(rol.Length - 1, 1);
            return rol;
        }

        public ActionResult Sepetim()
        {
            var sepetim = ctx.Liste.Where(c => c.KullaniciUsername == User.Identity.Name).ToList();
            List<Filmler> filmler = new List<Filmler>();

            foreach (var item in sepetim)
            {
                var film = ctx.Filmler.FirstOrDefault(c => c.FilmID == item.FilmID);
                filmler.Add(film);
            }

            return View(filmler);

        }

    }
}