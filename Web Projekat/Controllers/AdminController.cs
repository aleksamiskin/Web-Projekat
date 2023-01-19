using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Web.Mvc;
using Web_Projekat.Models;

namespace Web_Projekat.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null || korisnik.KorisnickoIme.Equals(""))
                return RedirectToAction("Index", "LoginRegister");

            ViewBag.Admin = true;
            return View();
        }

        public ActionResult RegistrujProdavca()
        {
            Korisnik korisnik = new Korisnik();
            Session["korisnik"] = korisnik;
            return View(korisnik);
        }

        [HttpPost]
        public ActionResult RegistrujProdavca(Korisnik korisnik)
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
            korisnik.TipKorisnika = Enums.TipKorisnika.NIJE_KUPAC;
            korisnik.UlogaKorisnika = Enums.UlogaKorisnika.PRODAVAC;

            korisnici.Add(korisnik);
            DataBase.SacuvajKorisnika(korisnik);
            Session["korisnik"] = korisnik;
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Korisnici()
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            return View(korisnici);
        }

        public ActionResult AktivirajManifestaciju()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            return View(manifestacije);
        }

        [HttpPost]
        public ActionResult AktivirajManifestaciju(string naziv)
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["Manifestacije"];

            foreach(Manifestacija m in manifestacije)
            {
                if (m.Naziv.Equals(naziv))
                {
                    m.StatusManifestacije = Enums.StatusManifestacije.AKTIVNO;
                }
            }

            string path = HostingEnvironment.MapPath("~/App_Data/Manifestacije.txt");
            FileStream stream = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);

            foreach (Manifestacija manifestacija in manifestacije)
            {
                sw.WriteLine(manifestacija.IDManifestacija+";"+manifestacija.Naziv + ";" + manifestacija.TipManifestacije + ";" 
                    + manifestacija.BrojMesta + ";" + manifestacija.DatumIVremeOdrzavanja + ";" 
                    + manifestacija.CenaKarte + ";"+ manifestacija.StatusManifestacije + ";" 
                    + manifestacija.MestoOdrzavanja.UlicaIBroj + ";" + manifestacija.MestoOdrzavanja.Mesto + ";" 
                    + manifestacija.MestoOdrzavanja.PostanskiBroj +";"+ manifestacija.KorisnickoIme + ";" + manifestacija.Validan);
            }

            sw.Close();
            stream.Close();

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public ActionResult ObrisiKorisnika(int IDKorisnika)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];

            //Update validity
            foreach (Korisnik korisnik in korisnici)
            {
                if (korisnik.IDKorisnika.Equals(IDKorisnika))
                    korisnik.Validan = false;
            }
            //---------------

            //Update korisnici.txt
            string path = HostingEnvironment.MapPath("~/App_Data/Korisnici.txt");
            FileStream stream = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);

            foreach (Korisnik k in korisnici)
            {
                sw.WriteLine(k.KorisnickoIme+ ";" + k.Lozinka+ ";" + k.Ime+ ";" + k.Prezime+ ";" +k.Pol + ";" + 
                    k.DatumRodjenja+ ";" + k.UlogaKorisnika+ ";" + k.Bodovi + ";" + k.TipKorisnika + ";" 
                    + k.IDKorisnika + ";" + k.Validan);
            }
            sw.Close();
            stream.Close();
            //----------------
            return RedirectToAction("Index");
        }
        public ActionResult Manifestacije()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            return View(manifestacije);
        }

        [HttpPost]
        public ActionResult ObrisiManifestaciju(int id)
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];

            //Update validity
            foreach (Manifestacija manifestacija in manifestacije)
            {
                if (manifestacija.IDManifestacija.Equals(id))
                    manifestacija.Validan = false;
            }
            //---------------

            //Update manifestacije.txt
            string path = HostingEnvironment.MapPath("~/App_Data/Manifestacije.txt");
            FileStream stream = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(stream);

            foreach (Manifestacija m in manifestacije)
            {
                sw.WriteLine(m.IDManifestacija +";"+ m.Naziv + ";" + m.TipManifestacije + ";" + m.BrojMesta+ ";" + m.DatumIVremeOdrzavanja+ ";" 
                    + m.CenaKarte+ ";" + m.StatusManifestacije + ";" + m.MestoOdrzavanja.UlicaIBroj +";"
                    + m.MestoOdrzavanja.Mesto+ ";" + m.MestoOdrzavanja.PostanskiBroj +";"+ m.KorisnickoIme + ";" 
                    + m.Validan);
            }
            sw.Close();
            stream.Close();
            //----------------
            return RedirectToAction("Index");
        }

        public ActionResult PretragaPoImenu(string ime)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> rezultat = new List<Korisnik>();

            if(ime.Equals(""))
            {
                ViewBag.Rezultat = korisnici;
            }
            else
            {
                foreach(var korisnik in korisnici)
                {
                    if(korisnik.Ime.Equals(ime))
                    {
                        rezultat.Add(korisnik);
                    }
                }
                ViewBag.Rezultat = rezultat;
            }
            return View("~/Views/Admin/Korisnici.cshtml");
        }

        public ActionResult PretragaPoPrezimenu(string prezime)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> rezultat = new List<Korisnik>();

            if (prezime.Equals(""))
            {
                ViewBag.Rezultat = korisnici;
            }
            else
            {
                foreach (var korisnik in korisnici)
                {
                    if (korisnik.Prezime.Equals(prezime))
                    {
                        rezultat.Add(korisnik);
                    }
                }
                ViewBag.Rezultat = rezultat;
            }
            return View("~/Views/Admin/Korisnici.cshtml");
        }

        public ActionResult PretragaPoKorisnickomImenu(string korisnickoIme)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> rezultat = new List<Korisnik>();

            if (korisnickoIme.Equals(""))
            {
                ViewBag.Rezultat = korisnici;
            }
            else
            {
                foreach (var korisnik in korisnici)
                {
                    if (korisnik.KorisnickoIme.Equals(korisnickoIme))
                    {
                        rezultat.Add(korisnik);
                    }
                }
                ViewBag.Rezultat = rezultat;
            }
            return View("~/Views/Admin/Korisnici.cshtml");
        }

        public ActionResult FiltriranjePoUloziKorisnika(string uloga)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> pretrazeni = new List<Korisnik>();
            Enums.UlogaKorisnika uloga1 = Enums.UlogaKorisnika.ADMINISTRATOR;


            if (uloga.Equals("ADMINISTRATOR"))
                uloga1 = Enums.UlogaKorisnika.ADMINISTRATOR;
            else if (uloga.Equals("PRODAVAC"))
                uloga1 = Enums.UlogaKorisnika.PRODAVAC;
            else if (uloga.Equals("KUPAC"))
                uloga1 = Enums.UlogaKorisnika.KUPAC;

            foreach (var korisnik in korisnici)
            {
                if (korisnik.UlogaKorisnika.Equals(uloga1))
                {
                    pretrazeni.Add(korisnik);
                }
            }
            ViewBag.Rezultat = pretrazeni;

            return View("~/Views/Admin/Korisnici.cshtml");
        }

        public ActionResult FiltritanjePoTipuKorisnika(string tip)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> pretrazeni = new List<Korisnik>();
            Enums.TipKorisnika tip1 = Enums.TipKorisnika.NIJE_KUPAC;


            if (tip.Equals("BRONZANI"))
                tip1 = Enums.TipKorisnika.BRONZANI;
            else if (tip.Equals("SREBRNI"))
                tip1 = Enums.TipKorisnika.SREBRNI;
            else if (tip.Equals("ZLATNI"))
                tip1 = Enums.TipKorisnika.ZLATNI;
            else if (tip.Equals("NIJE_KORISNIK"))
                tip1 = Enums.TipKorisnika.NIJE_KUPAC;

            foreach (var korisnik in korisnici)
            {
                if (korisnik.UlogaKorisnika.Equals(tip1))
                {
                    pretrazeni.Add(korisnik);
                }
            }
            ViewBag.Rezultat = pretrazeni;

            return View("~/Views/Admin/Korisnici.cshtml");
        }

        public ActionResult SortirajKorisnikePoImenu()
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            korisnici.Sort((a, b) => a.Ime.CompareTo(b.Ime));
            return View();
        }
        public ActionResult SortirajKorisnikePoPrezimenu()
        {
            return null;
        }
        public ActionResult SortirajKorisnikePoKorisnickomImenu()
        {
            return null;
        }

        public ActionResult SveKarte()
        {
            List<Karta> karte = (List<Karta>)HttpContext.Application["Karte"];
            
            return View(karte);
        }
    }
}