using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Web_Projekat.Models;

namespace Web_Projekat.Controllers
{
    public class KupacController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Karte()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme.Equals(""))
                return RedirectToAction("Index", "LoginRegister");
            ViewBag.Ime = korisnik.Ime;
            ViewBag.Prezime = korisnik.Prezime;
            List<Karta> karte = (List<Karta>)HttpContext.Application["karte"];
            
            foreach(Karta k in karte)
            {
                if (k.DatumIVreme < DateTime.Now)
                    k.Status = Enums.StatusKarte.REZERVISANA;
            }
            string path = HostingEnvironment.MapPath("~/App_Data/Karte.txt");
            FileStream stream = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);

            foreach (Karta karta in karte)
            {
                sw.WriteLine(karta.IdKarte + ";" + karta.NazivManifestacije + ";" + karta.DatumIVreme
                    + ";" + karta.Cena + ";" + karta.ImeKupca + ";" + karta.PrezimeKupca + ";" +
                    karta.Status + ";" + karta.Tip + ";" + karta.ImeProdavca);
            }

            sw.Close();
            stream.Close();


            return View(karte);
        }

        public ActionResult DodajKomentar()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme.Equals(""))
                return RedirectToAction("Index", "LoginRegister");
            return View();
        }

        [HttpPost]
        public ActionResult DodajKomentar(Komentar komentar)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme.Equals(""))
                return RedirectToAction("Index", "LoginRegister");

            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["listakomentara"];

            komentar.Kupac = korisnik.KorisnickoIme;

            komentari.Add(komentar);
            DataBase.SacuvajKomentar(komentar);
            return View();
        }
    }
}