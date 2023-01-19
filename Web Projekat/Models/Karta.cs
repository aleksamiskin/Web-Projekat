using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Projekat.Models
{
    public class Karta
    {
        public Karta(string idKarte, string nazivManifestacije, DateTime datumIVreme, 
            float cena, string imeKupca, string prezimeKupca, Enums.StatusKarte status, 
            Enums.TipKarte tip, string imeProdavca)
        {
            IdKarte = idKarte;            
            DatumIVreme = datumIVreme;
            Cena = cena;
            ImeKupca = imeKupca;
            PrezimeKupca = prezimeKupca;
            Status = status;
            Tip = tip;
            ImeProdavca = imeProdavca;
            NazivManifestacije = nazivManifestacije;
        }

        [Required]
        public string IdKarte { get; set; }
        [Required]
        public string NazivManifestacije { get; set; }
        [Required]
        public DateTime DatumIVreme { get; set; }
        //?
        [Required]
        public float Cena { get; set; }
        [Required]
        public string ImeKupca { get; set; }
        //?
        [Required]
        public string PrezimeKupca { get; set; }
        
        public Enums.StatusKarte Status { get; set; }
        public Enums.TipKarte Tip { get; set; }
        public string ImeProdavca { get; set; }
    }
}