using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmDukkaniProje.Controllers
{
    using App_Classes;
    using System.Web.Security;
    [Authorize]
    public class UyeController : Controller
    {
        // GET: Uye
        [AllowAnonymous]
       public ActionResult GirisYap()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(Kullanici k , string Hatirla)
        {
            bool sonuc = Membership.ValidateUser(k.KullaniciAdi, k.Parola);

            if (sonuc)
            {
                if (Hatirla == "on")
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, true);
                else
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, false);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı Adı veya Parola Hatalı";
                return View();
            }
        }

        public ActionResult CikisYap() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap");
        }

        [AllowAnonymous]

        public ActionResult ParolamiUnuttum()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]

        public ActionResult ParolamiUnuttum(Kullanici k)
        {
            MembershipUser mu = Membership.GetUser(k.KullaniciAdi);
            if (mu.PasswordQuestion==k.GizliSoru)
            {
                string pwd = mu.GetPassword(k.GizliCevap);
                mu.ChangePassword(pwd, k.Parola);
                return RedirectToAction("GirisYap");
            }
            else
            {
                ViewBag.Mesaj = "Girilen bilgiler yanlıştır!";
                return View();
            }
        }
    }
}