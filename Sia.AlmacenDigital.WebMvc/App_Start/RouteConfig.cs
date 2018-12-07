using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sia.AlmacenDigital.WebMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Account",
                url: "Account/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Admin",
              url: "Admin/{action}/{id}",
              defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Banner",
                url: "Banner/{action}/{id}",
                defaults: new { controller = "Banner", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Manage",
                url: "Manage/{action}/{id}",
                defaults: new { controller = "Manage", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Producto",
                url: "Producto/{action}/{id}",
                defaults: new { controller = "Producto", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("ActionOnly",
                "{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                new
                {
                    action = "Index|Promocion|Memoria|PowerBank|Audio|Tecnologia|Varios|Catalogo|DetalleProducto|DetalleCodigo"
                }
            );



           // routes.MapRoute("ActionOnly",
           //    "{action}/{id}",
           //    new
           //    {
           //        controller = "Politica",
           //        action = "PoliticaGarantia",
           //        id = UrlParameter.Optional
           //    },
           //    new
           //    {
           //        action = "NuestraHistoria|AvisoPrivacidad"
           //    }
           //);


            routes.MapRoute(
               name: "Politica",
               url: "Politica/{action}",
               defaults: new { controller = "Politica", action = "PoliticaGarantia|NuestraHistoria|AvisoPrivacidad", id = UrlParameter.Optional }
           );
        }
    }
}

