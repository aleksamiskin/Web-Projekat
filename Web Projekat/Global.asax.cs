using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web_Projekat.Models;

namespace Web_Projekat
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            List<Manifestacija> manifestacije = DataBase.UcitajManifestacije("~/App_Data/Manifestacije.txt");
            HttpContext.Current.Application["manifestacije"] = manifestacije;

            List<Korisnik> korisnici= DataBase.CitajKorisnike("~/App_Data/Korisnici.txt");
            HttpContext.Current.Application["korisnici"] = korisnici;

            List<Karta> karte = DataBase.UcitajKarte("~/App_Data/Karte.txt");
            HttpContext.Current.Application["karte"] = karte;

            List<Komentar> listakomentara = DataBase.UcitajKomentar("~/App_Data/Komentari.txt");
            HttpContext.Current.Application["listakomentara"] = listakomentara;
        }
    }
}
