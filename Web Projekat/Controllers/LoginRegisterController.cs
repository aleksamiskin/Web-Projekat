using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Projekat.Models;

namespace Web_Projekat.Controllers
{
    public class LoginRegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string korisnickoIme, string lozinka)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["Korisnici"];
            Korisnik korisnik = korisnici.Find(u => u.KorisnickoIme.Equals(korisnickoIme) && u.Lozinka.Equals(lozinka));
            

            if (korisnik == null)
            {
                ViewBag.Message = $"Korisnik ne postoji!";
                return View("Index");
            }

            if (!korisnik.Validan)
            {
                ViewBag.Message = $"Korisnik nema pristup, kontaktirajte admina za detalje.";
                return View("Index");
            }


            if(korisnik.UlogaKorisnika.Equals((Enums.UlogaKorisnika)Enum.Parse(typeof(Enums.UlogaKorisnika),"KUPAC")))
            {
                Session["korisnik"] = korisnik;
                return RedirectToAction("Index", "Kupac");
            }

            if (korisnik.UlogaKorisnika.Equals((Enums.UlogaKorisnika)Enum.Parse(typeof(Enums.UlogaKorisnika), "PRODAVAC")))
            {
                Session["korisnik"] = korisnik;
                return RedirectToAction("Index", "Prodavac");
            }

            if (korisnik.UlogaKorisnika.Equals((Enums.UlogaKorisnika)Enum.Parse(typeof(Enums.UlogaKorisnika), "ADMINISTRATOR")))
            {
                Session["korisnik"] = korisnik;
                ViewBag.Admin = true;
                return RedirectToAction("Index", "Admin");
            }
           
            Session["korisnik"] = korisnik;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            Korisnik korisnik = new Korisnik();
            Session["korisnik"] = korisnik;
            return View(korisnik);
        }

        [HttpPost]
        public ActionResult Register(Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Greska pri unosu!";
                return View();
            }

            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["Korisnici"];
            if (korisnici.Contains(korisnik))
            {
                ViewBag.Message = $"Korisnik {korisnik.KorisnickoIme} vec postoji!";
                return View();
            }
            int broj = korisnici.Count + 1;
            korisnik.IDKorisnika = broj;
            korisnik.Bodovi = 0;
            korisnik.TipKorisnika = Enums.TipKorisnika.BRONZANI;
            korisnik.UlogaKorisnika = Enums.UlogaKorisnika.KUPAC;

            korisnici.Add(korisnik);
            DataBase.SacuvajKorisnika(korisnik);
            Session["korisnik"] = korisnik;
            return RedirectToAction("Index", "LoginRegister");
        }
    }
}