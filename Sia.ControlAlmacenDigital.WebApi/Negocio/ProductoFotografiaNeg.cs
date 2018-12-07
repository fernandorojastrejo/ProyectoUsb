using Sia.ControlAlmacenDigital.Entidades;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace Sia.ControlAlmacenDigital.Negocio
{
    public class ProductoFotografiaNeg : BaseNeg<ProductoFotografia>
    {
        //public Resultado<ProductoFotografia> SubirImagenProducto()
        //{
        //    Resultado<ProductoFotografia> resultado = new Resultado<ProductoFotografia>();
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    try
        //    {

        //        var httpRequest = HttpContext.Current.Request;

        //        foreach (string file in httpRequest.Files)
        //        {
        //            var postedFile = httpRequest.Files[file];
        //            if (postedFile != null && postedFile.ContentLength > 0)
        //            {

        //                int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

        //                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        //                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
        //                var extension = ext.ToLower();
        //                if (!AllowedFileExtensions.Contains(extension))
        //                {
        //                    resultado.Error = true;
        //                    resultado.MensajeError = WebApi.Recursos.ErrorFormatoFoto;
        //                }
        //                else if (postedFile.ContentLength > MaxContentLength)
        //                {

        //                    var message = string.Format(" Peso máximo de la imagen 1 mb.");
        //                    resultado.Error = true;
        //                    resultado.MensajeError = WebApi.Recursos.ErrorPesoMaxFoto;
        //                }
        //                else
        //                {



        //                    var filePath = HttpContext.Current.Server.MapPath("~/Images/Productos/" + postedFile.FileName);

        //                    postedFile.SaveAs(filePath);

        //                }
        //            }
        //            resultado.Exito = true;
        //            resultado.MensajeExito = WebApi.Recursos.ExitoAgregarFoto;
        //        }
        //        //var res = string.Format(" Seleccione una imagen.");
        //        //resultado.Error = true;
        //        //resultado.MensajeError = WebApi.Recursos.ErrorAgregar + res;
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado.Error = true;
        //        resultado.MensajeError = WebApi.Recursos.ErrorAgregarFoto + ex;
        //    }
        //    return resultado;
        //}
        public override Resultado<ProductoFotografia> Agregar(ProductoFotografia modelo)
        {
            Resultado<ProductoFotografia> resultado = new Resultado<ProductoFotografia>();

            WebApi.ProductoFotografia productoNuevo = new WebApi.ProductoFotografia();

            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    ObjectExtensions.CopyObject(productoNuevo, modelo);
                    contexto.ProductoFotografia.Add(productoNuevo);
                    contexto.SaveChanges();
                    resultado.Exito = true;
                    resultado.MensajeExito = WebApi.Recursos.ExitoAgregar;
                }
            }
            catch (Exception ex)
            {
                resultado.Error = true;
                resultado.MensajeError = WebApi.Recursos.ErrorAgregar + ' ' + ex.Message;
            }
            return resultado;
        }

        public override Resultado<ProductoFotografia> Editar(ProductoFotografia modelo)
        {
            throw new NotImplementedException();
        }

        public override Resultado<ProductoFotografia> Eliminar(int id)
        {
            Resultado<ProductoFotografia> resultado = new Resultado<ProductoFotografia>();

            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    var itemToRemove = contexto.ProductoFotografia.SingleOrDefault(x => x.ProductoFotoId == id);
                    if (itemToRemove != null)
                    {
                        contexto.ProductoFotografia.Remove(itemToRemove);
                        contexto.SaveChanges();
                        resultado.Exito = true;
                        resultado.MensajeExito = WebApi.Recursos.ExitoEliminar;
                    }
                    else
                    {
                        resultado.Error = true;
                        resultado.MensajeError = WebApi.Recursos.ErrorEliminar;
                    }

                }
            }
            catch (Exception ex)
            {
                resultado.Error = true;
                resultado.MensajeError = WebApi.Recursos.ErrorAgregar + ' ' + ex.Message;
            }
            return resultado;
        }

        public override List<ProductoFotografia> Obtener()
        {
            throw new NotImplementedException();
        }

        public override ProductoFotografia Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ProductoFotografia> Obtener(int id, string anioId)
        {
            throw new NotImplementedException();
        }

        public List<ProductoFotografia> ObtenerProductoFotos(int id)
        {
            List<ProductoFotografia> resultado = new List<ProductoFotografia>();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.ProductoFotografia
                        .Where(p => p.ProductoId == id)
                        .Select(p => new ProductoFotografia() {
                            ProductoFotoId = p.ProductoFotoId,
                            ProductoId = p.ProductoId,
                            UrlImagen = p.UrlImagen
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                var msj = ex;
                throw;
            }
            return resultado;
        }
    }
}