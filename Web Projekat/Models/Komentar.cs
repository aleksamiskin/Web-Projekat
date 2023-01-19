using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_Projekat.Models
{
    public class Komentar
    {
        public Komentar()
        {
            Kupac = "";
            Manifestacija = "";
            TekstKomentara = "";
            Ocena = 0;
            Odobren = false;
        }

        public Komentar(string kupac, string manifestacija, string tekstKomentara, int ocena, bool odobren)
        {
            Kupac = kupac;
            Manifestacija = manifestacija;
            TekstKomentara = tekstKomentara;
            Ocena = ocena;
            Odobren = odobren;
        }

        public string Kupac { get; set; }
        [Required]
        public string Manifestacija { get; set; }
        [Required]
        public string TekstKomentara { get; set; }
        [Required]
        [Range(1,5,ErrorMessage = "Unesite ocenu od 1 do 5")]
        public int Ocena { get; set; }
        public bool Odobren { get; set; } = false;
    }
}