using Sia.AlmacenDigital.WebMvc.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sia.AlmacenDigital.WebMvc.Controllers
{
    [Authorize]
    [MyAuthorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Producto
        public ActionResult Producto()
        {
            return View();
        }

        // GET: Cliente
        public ActionResult Cliente()
        {
            return View();
        }

        // GET: Proveedores
        public ActionResult Proveedor()
        {
            return View();
        }

        // GET: Ordenes
        public ActionResult Orden()
        {
            return View();
        }

        public ActionResult Banner()
        {
            return View();
        }       

        [Route("Admin/Producto/Agregar")]
        public ActionResult ProductoAgregar()
        {
            return View();
        }

        [Route("Admin/Producto/{id}")]
        public ActionResult ProductoEditar(int id)
        {
            ViewBag.ProductoId = id;
            return View();
        }

        public ActionResult UsuarioSistema()
        {
            return View();
        }

        [Route("Admin/UsuarioSistema/{id}")]
        public ActionResult UsuarioEditar(string id)
        {
            ViewBag.UsuarioId = id;
            return View();
        }

        
        public ActionResult ProductoExcel()
        {
            return View();
        }

        public ActionResult SaveUploadedFile()
        {
            bool esArchivoValido = false;
            var resultado = "";
            string fName = string.Empty;
            string pathString = string.Empty;
            string folderCategoria = string.Empty;

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
            
                    pathString = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Excel\\";
                    file.SaveAs(pathString + "\\" + file.FileName);

                    string sourcePath = @"C:\inetpub\wwwroot\UsbTechnology\Excel\";
                    string targetPath = @"C:\inetpub\wwwroot\UsbTechnologyApi\Excel\";

                    // Use Path class to manipulate file and directory paths.
                    string sourceFile = System.IO.Path.Combine(sourcePath, file.FileName);
                    string destFile = System.IO.Path.Combine(targetPath, file.FileName);

                    System.IO.File.Copy(sourceFile, destFile, true);

                    esArchivoValido = true;
                    resultado = "ok";
                }
            }

            if (esArchivoValido)
            {
                resultado = "ok";
            }
            else resultado = "NoArchivo";
            return Json(new { Message = resultado });
        }

    }
}