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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            return View(manifestacije);
        }

        public ActionResult VidiProfil()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme.Equals(""))
                return RedirectToAction("Index", "LoginRegister");

            ViewBag.VidiProfil = korisnik;
            return View();
        }

        public ActionResult IzmeniPodatke()
        {
            Korisnik trenutni = (Korisnik)Session["korisnik"];
            if (trenutni == null || trenutni.KorisnickoIme.Equals(""))
                return RedirectToAction("Index", "LoginRegister");

            ViewBag.Promena = trenutni;
            return View();
        }

        [HttpPost]
        public ActionResult IzmeniPodatke(Korisnik korisnik)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["Korisnici"];

            foreach (Korisnik k in korisnici)
            {
                if (k.IDKorisnika.Equals(korisnik.IDKorisnika))
                {
                    k.KorisnickoIme = korisnik.KorisnickoIme;
                    k.Lozinka = korisnik.Lozinka;
                    k.Ime = korisnik.Ime;
                    k.Prezime = korisnik.Prezime;
                    k.Pol = korisnik.Pol;
                    k.DatumRodjenja = korisnik.DatumRodjenja;
                }
            }

            string path = HostingEnvironment.MapPath("~/App_Data/Korisnici.txt");
            FileStream stream = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);

            foreach (Korisnik k in korisnici)
            {
                sw.WriteLine(k.KorisnickoIme + ";" + k.Lozinka + ";" + k.Ime + ";" + k.Prezime + ";" + k.Pol + ";" + k.DatumRodjenja
                    + ";" + k.UlogaKorisnika + ";" + k.Bodovi + ";" + k.TipKorisnika + ";" + k.IDKorisnika);
            }

            sw.Close();
            stream.Close();
            return RedirectToAction("Index", "LoginRegister");
        }
        
        [HttpPost]
        public ActionResult Pretraga(string naziv, DateTime datum, double cena, string mesto)
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> rezultatPretrage = new List<Manifestacija>();

            if(naziv.Equals("") && datum.Equals("") && cena.Equals("") && mesto.Equals(""))
            {
                ViewBag.pretraga = manifestacije;
            }
            else
            {
                foreach(Manifestacija manifestacija in manifestacije)
                {
                    if (manifestacija.Naziv.Equals(naziv) || manifestacija.DatumIVremeOdrzavanja.Equals(datum) || 
                        manifestacija.CenaKarte.Equals(cena) || manifestacija.MestoOdrzavanja.Mesto.Equals(mesto))
                    {
                        rezultatPretrage.Add(manifestacija);
                    }
                }
            }

            ViewBag.pretraga = rezultatPretrage;

            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult Manifestacije()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            return View(manifestacije);
        }

        public ActionResult Detalji()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            return View(manifestacije);
        }

        [HttpPost]
        public ActionResult Detalji(int id)
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
               
            foreach (Manifestacija item in manifestacije)
            {
                if (item.IDManifestacija.Equals(id))
                {
                    ViewBag.Naziv = item.Naziv;
                    ViewBag.TipManifestacije = item.TipManifestacije;
                    ViewBag.BrojMesta = item.BrojMesta;
                    ViewBag.DatumIVremeOdrzavanja = item.DatumIVremeOdrzavanja;
                    ViewBag.CenaKarte = item.CenaKarte;
                    ViewBag.MestoOdrzavanja = item.MestoOdrzavanja.Mesto;
                    ViewBag.StatusManifestacije = item.StatusManifestacije;
                }
            }
            

            return View();
        }
    }
}