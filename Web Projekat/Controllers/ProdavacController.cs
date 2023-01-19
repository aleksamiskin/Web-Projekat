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
    public class ProdavacController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KreirajManifestaciju()
        {
           return View();
        }

        [HttpPost]
        public ActionResult KreirajManifestaciju(Manifestacija manifestacija)
        {
            /*if (!ModelState.IsValid)
            {
                ViewBag.Error = "Input error!";
                return View();
            }*/

            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            if (manifestacije.Contains(manifestacija))
            {
                ViewBag.Message = $"Vec postoji manifestacije sa datim vremenom i lokacijom.";
                return View();
            }

            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme.Equals(""))
            {
                return RedirectToAction("Index", "Prodavac");
            }

            manifestacija.KorisnickoIme = korisnik.KorisnickoIme;
            manifestacija.StatusManifestacije = Enums.StatusManifestacije.NEAKTIVNO;
            manifestacije.Add(manifestacija);
            DataBase.SacuvajManifestaciju(manifestacija);
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult Manifestacije()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme.Equals(""))
                return RedirectToAction("Index", "Prodavac");

            ViewBag.Korisnik = korisnik.KorisnickoIme;
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["Manifestacije"];

            return View(manifestacije);
            
        }

        public ActionResult RezervisaneKarte()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme.Equals(""))
                return RedirectToAction("Index", "Prodavac");

            ViewBag.Prodavac = korisnik.KorisnickoIme;
            List<Karta> karte = (List<Karta>)HttpContext.Application["Karte"];
            return View(karte);
        }

        public ActionResult OdobrenjeKomentara()
        {
            List<Komentar> komentar = (List<Komentar>)HttpContext.Application["listakomentara"];
            return View(komentar);
        }

        [HttpPost]
        public ActionResult OdobrenjeKomentara(string manifestacija)
        {
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["listakomentara"];

            foreach(Komentar komentar in komentari)
            {
                if(komentar.Manifestacija.Equals(manifestacija))
                {
                    komentar.Odobren = true;
                }
            }

            string path = HostingEnvironment.MapPath("~/App_Data/Komentari.txt");
            FileStream stream = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);

            foreach (Komentar komentar in komentari)
            {
                sw.WriteLine(komentar.Kupac +";"+ komentar.Manifestacija +";"+ komentar.TekstKomentara
                    +";"+ komentar.Ocena +";"+ komentar.Odobren);
            }

            sw.Close();
            stream.Close();

            return RedirectToAction("Index", "Prodavac");
        }
    }
}