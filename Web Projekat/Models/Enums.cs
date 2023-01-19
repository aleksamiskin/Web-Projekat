using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Projekat.Models
{
    public class Enums
    {
        public enum UlogaKorisnika
        {
            ADMINISTRATOR,
            PRODAVAC,
            KUPAC
        }

        public enum TipKorisnika
        {
            ZLATNI,
            SREBRNI,
            BRONZANI,
            NIJE_KUPAC
        }

        public enum TipManifestacije
        {
            KONCERT,
            FESTIVAL,
            POZORISTE
        }

        public enum StatusManifestacije
        {
            AKTIVNO,
            NEAKTIVNO
        }

        public enum StatusKarte
        {
            REZERVISANA,
            ODUSTANAK
        }

        public enum TipKarte
        {
            VIP,
            REGULAR,
            FANPIT
        }

        public enum PolKorisnika
        {
            MUSKI,
            ZENSKI
        }
    }
}