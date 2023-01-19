using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Projekat.Models
{
    public class MestoOdrzavanja
    {
        public string UlicaIBroj { get; set; }
        public string Mesto { get; set; }
        public int PostanskiBroj { get; set; }

        public MestoOdrzavanja() { }

        public MestoOdrzavanja(string ulicaIBroj, string mesto, int postanskiBroj)
        {
            UlicaIBroj = ulicaIBroj;
            Mesto = mesto;
            PostanskiBroj = postanskiBroj;
        }
    }
}