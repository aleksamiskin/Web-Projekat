using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Projekat.Models
{
    public class Korisnik
    {
        [Required]
        public string KorisnickoIme { get; set; }
        [Required]
        public string Lozinka { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public Enums.PolKorisnika Pol { get; set; }
        [Required]
        public DateTime DatumRodjenja { get; set; }
        [Required]
        public Enums.UlogaKorisnika UlogaKorisnika { get; set; }
        [Required]
        public int Bodovi { get; set; }
        [Required]
        public Enums.TipKorisnika TipKorisnika {get;set;}
        [Required]
        public int IDKorisnika { get; set; }
        [Required]
        public bool Validan { get; set; } = true;

        public Korisnik()
        {
            KorisnickoIme = "";
            Lozinka = "";
            TipKorisnika = Enums.TipKorisnika.NIJE_KUPAC;
        }

        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, Enums.PolKorisnika pol, 
            DateTime datumRodjenja, Enums.UlogaKorisnika ulogaKorisnika, int bodovi, Enums.TipKorisnika tipKorisnika, int idKorisnika, bool validan)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            DatumRodjenja = datumRodjenja;
            UlogaKorisnika = ulogaKorisnika;
            Bodovi = bodovi;
            TipKorisnika = tipKorisnika;
            IDKorisnika = idKorisnika;
            Validan = validan;
        }
    }
}