using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Projekat.Models
{
    public class Lokacija
    {
        public double XKoordinata { get; set; }
        public double YKoordinata { get; set; }
        public MestoOdrzavanja MestoOdrzavanja { get; set; }

        public Lokacija(double xKoordinata, double yKoordinata, MestoOdrzavanja mestoOdrzavanja)
        {
            XKoordinata = xKoordinata;
            YKoordinata = YKoordinata;
            MestoOdrzavanja = mestoOdrzavanja;
        }
    }
}