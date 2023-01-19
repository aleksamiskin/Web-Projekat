using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Projekat.Models
{
    public class Manifestacija
    {
        
        [Required]
        public string Naziv { get; set; }
        [Required]
        public Enums.TipManifestacije TipManifestacije { get; set; }
        [Required]
        public int BrojMesta { get; set; }
        [Required]
        public DateTime DatumIVremeOdrzavanja { get; set; }
        [Required]
        public double CenaKarte { get; set; }
        
        public Enums.StatusManifestacije StatusManifestacije { get; set; }
        [Required]
        public MestoOdrzavanja MestoOdrzavanja{get;set;}
        //poster? (slika)
        
        public string KorisnickoIme { get; set; }
        
        public bool Validan { get; set; } = true;

        public int IDManifestacija { get; set; }

        public int Ocena { get; set; }

        public Manifestacija() {}

        public Manifestacija(int idManifestacija, string naziv, Enums.TipManifestacije tipManifestacije, 
            int brojMesta, DateTime datumIVremeOdrzavanja, double cenaKarte, 
            Enums.StatusManifestacije statusManifestacije, MestoOdrzavanja mestoOdrzavanja, 
            string korisnickoIme, bool validan)
        {
            Naziv = naziv;
            TipManifestacije = tipManifestacije;
            BrojMesta = brojMesta;
            DatumIVremeOdrzavanja = datumIVremeOdrzavanja;
            CenaKarte = cenaKarte;
            StatusManifestacije = statusManifestacije;
            KorisnickoIme = korisnickoIme;
            MestoOdrzavanja = mestoOdrzavanja;
            Validan = validan;
            IDManifestacija = idManifestacija;
        }
    }
}