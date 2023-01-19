using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Web_Projekat.Models
{
    public class DataBase
    {
        public static List<Manifestacija> UcitajManifestacije(string path)
        {
            List<Manifestacija> manifestacije = new List<Manifestacija>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Manifestacija p = new Manifestacija(int.Parse(tokens[0]),tokens[1],
                    (Enums.TipManifestacije)Enum.Parse(typeof(Enums.TipManifestacije),tokens[2]),
                    int.Parse(tokens[3]),DateTime.Parse(tokens[4]),double.Parse(tokens[5]),
                    (Enums.StatusManifestacije)Enum.Parse(typeof(Enums.StatusManifestacije),tokens[6]),
                    new MestoOdrzavanja(tokens[7],tokens[8],int.Parse(tokens[9])),
                    tokens[10],bool.Parse(tokens[11]));
                manifestacije.Add(p);
            }
            sr.Close();
            stream.Close();

            //sortiranje
            manifestacije.Sort((x, y) => x.DatumIVremeOdrzavanja.CompareTo(y.DatumIVremeOdrzavanja));

            return manifestacije;
        }

        public static List<Korisnik> CitajKorisnike(string path)
        {
            List<Korisnik> users = new List<Korisnik>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Korisnik k = new Korisnik(tokens[0], tokens[1], tokens[2], tokens[3],
                    (Enums.PolKorisnika)Enum.Parse(typeof(Enums.PolKorisnika), tokens[4]),
                    DateTime.Parse(tokens[5]), (Enums.UlogaKorisnika)Enum.Parse(typeof(Enums.UlogaKorisnika), tokens[6]),
                    int.Parse(tokens[7]), (Enums.TipKorisnika)Enum.Parse(typeof(Enums.TipKorisnika), tokens[8]),
                    int.Parse(tokens[9]), bool.Parse(tokens[10]));
                users.Add(k);
            }
            sr.Close();
            stream.Close();

            return users;
        }

        public static void SacuvajKorisnika(Korisnik korisnik)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Korisnici.txt");
            FileStream stream = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(stream);

            sw.WriteLine(korisnik.KorisnickoIme + ";" + korisnik.Lozinka + ";" + korisnik.Ime + ";" + korisnik.Prezime + ";"
                + korisnik.Pol + ";" + korisnik.DatumRodjenja + ";" + korisnik.UlogaKorisnika + ";" + korisnik.Bodovi + ";"
                + korisnik.TipKorisnika + ";" + korisnik.IDKorisnika + ";" + korisnik.Validan);

            sw.Close();
            stream.Close();
        }

        public static void SacuvajManifestaciju(Manifestacija manifestacija)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Manifestacije.txt");
            FileStream stream = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(stream);

            sw.WriteLine(manifestacija.IDManifestacija +";"+ manifestacija.Naziv + ";" + manifestacija.TipManifestacije + ";" + manifestacija.BrojMesta +
                ";" + manifestacija.DatumIVremeOdrzavanja + ";" + manifestacija.CenaKarte + ";" + manifestacija.StatusManifestacije
                +";"+  manifestacija.MestoOdrzavanja.UlicaIBroj + ";" +
                manifestacija.MestoOdrzavanja.Mesto + ";" + manifestacija.MestoOdrzavanja.PostanskiBroj
                + ";" + manifestacija.KorisnickoIme + ";" + manifestacija.Validan);

            sw.Close();
            stream.Close();
        }

        public static List<Karta> UcitajKarte(string path)
        {
            List<Karta> karte = new List<Karta>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Karta k = new Karta(tokens[0], tokens[1], DateTime.Parse(tokens[2]),
                    float.Parse(tokens[3]),tokens[4],tokens[5],
                    (Enums.StatusKarte)Enum.Parse(typeof(Enums.StatusKarte), tokens[6]),
                    (Enums.TipKarte)Enum.Parse(typeof(Enums.TipKarte), tokens[7]), tokens[8]);
                karte.Add(k);
            }
            sr.Close();
            stream.Close();

            return karte;
        }
        
        public static void SacuvajKomentar(Komentar komentar)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/Komentari.txt");
            FileStream stream = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(stream);

            sw.WriteLine(komentar.Kupac + ";" + komentar.Manifestacija + ";" + komentar.TekstKomentara +
                ";" + komentar.Ocena + ";" + komentar.Odobren);

            sw.Close();
            stream.Close();
        }

        public static List<Komentar> UcitajKomentar(string path)
        {
            List<Komentar> komentar = new List<Komentar>();
            path = HostingEnvironment.MapPath(path);
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Komentar k = new Komentar(tokens[0], tokens[1], tokens[2], int.Parse(tokens[3]), bool.Parse(tokens[4]));
                komentar.Add(k);
            }
            sr.Close();
            stream.Close();

            return komentar;
        }
    }
}