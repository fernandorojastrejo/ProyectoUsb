using Sia.ControlAlmacenDigital.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sia.ControlAlmacenDigital.Negocio
{
    public class CategoriaNeg : BaseNeg<Categoria>
    {
        public override Resultado<Categoria> Agregar(Categoria modelo)
        {
            throw new NotImplementedException();
        }

        public Resultado<Categoria> AgregarBannerTiempo(Categoria modelo)
        {
            Resultado<Categoria> resultado = new Resultado<Categoria>();

            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    var categoriaEncontrada = contexto.Categoria.Where(p => p.CategoriaId == modelo.CategoriaId).FirstOrDefault();
                    //contexto.Entry(modelo).State = EntityState.Modified;
                    categoriaEncontrada.TiempoBanner = modelo.TiempoBanner;
                    contexto.SaveChanges();
                    resultado.Exito = true;
                    resultado.MensajeExito = WebApi.Recursos.ExitoEditar;
                }
            }
            catch (Exception ex)
            {
                resultado.Error = true;
                resultado.MensajeError = WebApi.Recursos.ErrorEditar + ' ' + ex.Message;
            }
            return resultado;
        }

        public override Resultado<Categoria> Editar(Categoria modelo)
        {
            throw new NotImplementedException();
        }

        public override Resultado<Categoria> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Categoria> Obtener()
        {
            List<Categoria> resultado = new List<Categoria>();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.Categoria
                        .Select(c => new Categoria()
                        {
                            CategoriaId = c.CategoriaId,
                            Descripcion = c.Descripcion,
                            TiempoBanner = c.TiempoBanner
                        }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public override Categoria Obtener(int id)
        {
            Categoria resultado = new Categoria();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.Categoria
                        .Where(x => x.CategoriaId == id)
                        .Select(c => new Categoria()
                        {
                            CategoriaId = c.CategoriaId,
                            Descripcion = c.Descripcion,
                            TiempoBanner = c.TiempoBanner
                        }).FirstOrDefault();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }

        public override List<Categoria> Obtener(int id, string anioId)
        {
            throw new NotImplementedException();
        }
    }
}