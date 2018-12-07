using Sia.ControlAlmacenDigital.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sia.ControlAlmacenDigital.Negocio
{
    public class BannerNeg : BaseNeg<Banner>
    {
        public override Resultado<Banner> Agregar(Banner modelo)
        {
            Resultado<Banner> resultado = new Resultado<Banner>();

            WebApi.Banner bannerNuevo = new WebApi.Banner();

            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    ObjectExtensions.CopyObject(bannerNuevo, modelo);
                    contexto.Banner.Add(bannerNuevo);
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

        public override Resultado<Banner> Editar(Banner modelo)
        {
            throw new NotImplementedException();
        }

        public override Resultado<Banner> Eliminar(int id)
        {
            Resultado<Banner> resultado = new Resultado<Banner>();

            //WebApi.Banner bannerEliminar = new WebApi.Banner();

            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    var itemToRemove = contexto.Banner.SingleOrDefault(x => x.BannerId == id);
                    if (itemToRemove != null)
                    {
                        contexto.Banner.Remove(itemToRemove);
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

        public override List<Banner> Obtener()
        {
            throw new NotImplementedException();
        }

        public override Banner Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Banner> Obtener(int id, string anioId)
        {
            throw new NotImplementedException();
        }

        public List<Banner> ObtenerPorCategoria(int id)
        {
            List<Banner> resultado = new List<Banner>();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.Banner
                        .Where(x => x.CategoriaId == id)
                        .Select(b => new Banner()
                        {
                            BannerId = b.BannerId,
                            Categoria = new Categoria()
                            {
                                CategoriaId = b.Categoria.CategoriaId,
                                Descripcion = b.Categoria.Descripcion,
                                TiempoBanner = b.Categoria.TiempoBanner
                            },
                            UrlImagen = b.UrlImagen
                        }).ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
    }
}