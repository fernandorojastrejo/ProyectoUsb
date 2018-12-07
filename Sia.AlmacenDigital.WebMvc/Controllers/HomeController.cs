using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sia.AlmacenDigital.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sia.AlmacenDigital.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        public int GetTipoPrecioUsuario()
        {
            int precio = 0;
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
                precio = currentUser.TipoPrecio;
            }
            
            return precio;
        }
        public ActionResult Index()
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            ViewBag.CategoriaId = 6;
            return View();
        }

        //GET: Promociones
        public ActionResult Promocion()
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            ViewBag.CategoriaId = 7;
            return View();
        }

        //GET: Memorias USB

        public ActionResult Memoria()
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            ViewBag.CategoriaId = 1;
            return View();
        }

        //GET: Power Bank

        public ActionResult PowerBank()
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            ViewBag.CategoriaId = 2;
            return View();
        }

        //GET: Audio

        public ActionResult Audio()
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            ViewBag.CategoriaId = 3;
            return View();
        }

        //GET: Tecnología

        public ActionResult Tecnologia()
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            ViewBag.CategoriaId = 4;
            return View();
        }

        //GET: Varios

        public ActionResult Varios()
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            ViewBag.CategoriaId = 5;
            return View();
        }

        //GET: Catálogo

        public ActionResult Catalogo()
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            return View();
        }

        // GET: Perfil del usuario

        public ActionResult Perfil()
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            return View();
        }

        // GET: Detalle del producto
        public ActionResult DetalleProducto(int id)
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            ViewBag.ProductoId = id;
            return View();
        }

        //GET: Detalle del codigo padre
        [Route("DetalleCodigo/{codigo?}")]
        public  ActionResult DetalleCodigo(String codigo)
        {
            ViewBag.TipoPrecio = GetTipoPrecioUsuario();
            ViewBag.CodigoProducto = codigo;
            return View();
        }
    }
}