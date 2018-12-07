using Sia.ControlAlmacenDigital.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace Sia.ControlAlmacenDigital.Negocio
{
    public class ProductoNeg : BaseNeg<Producto>
    {
        public override List<Producto> Obtener()
        {
            List<Producto> resultado = new List<Producto>();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.Producto
                        .Select(p => new Producto()
                        {
                            ProductoId = p.ProductoId,
                            Categoria = new Categoria()
                            {
                                CategoriaId = p.Categoria.CategoriaId,
                                Descripcion = p.Categoria.Descripcion,
                                TiempoBanner = p.Categoria.TiempoBanner
                            },
                            Codigo = p.Codigo,
                            CodigoBarra = p.CodigoBarra,
                            Nombre = p.Nombre,
                            Pieza = p.Pieza,
                            Color = p.Color,
                            Material = p.Material,
                            Capacidad = p.Capacidad,
                            Descripcion = p.Descripcion,
                            Activo = p.Activo,
                            Medida = p.Medida,
                            EsPrincipal = p.EsPrincipal,
                            CodigoPrincipal = p.CodigoPrincipal,
                            Precio = p.Precio.Select(pr => new Precio()
                            {
                                ProductoId = pr.ProductoId,
                                Precio1 = pr.Precio1
                            }).ToList(),
                            ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                            {
                                ProductoFotoId = pf.ProductoFotoId,
                                ProductoId = pf.ProductoFotoId,
                                UrlImagen = pf.UrlImagen
                            }).ToList()

                        }).ToList();

                }
            }
            catch (Exception ex)
            {
                var msj = ex;
                throw;
            }
            return resultado;
        }

        public override Producto Obtener(int id)
        {
            Producto resultado = new Producto();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.Producto
                        .Where(x => x.ProductoId == id)
                        .Select(p => new Producto()
                        {
                            ProductoId = p.ProductoId,
                            Categoria = new Categoria()
                            {
                                CategoriaId = p.Categoria.CategoriaId,
                                Descripcion = p.Categoria.Descripcion,
                                TiempoBanner = p.Categoria.TiempoBanner
                            },
                            Codigo = p.Codigo,
                            CodigoBarra = p.CodigoBarra,
                            Nombre = p.Nombre,
                            Pieza = p.Pieza,
                            Descripcion = p.Descripcion,
                            Medida = p.Medida,
                            EsPrincipal = p.EsPrincipal,
                            CodigoPrincipal = p.CodigoPrincipal,
                            Precio = p.Precio.Select(pr => new Precio()
                            {
                                PrecioId = pr.PrecioId,
                                ProductoId = pr.ProductoId,
                                Precio1 = pr.Precio1,
                                Precio2 = pr.Precio2,
                                Precio3 = pr.Precio3,
                                Mes = pr.Mes,
                                ayo = pr.ayo,
                                Emitio = pr.Emitio
                            }).ToList(),
                            Color = p.Color,
                            Capacidad = p.Capacidad,
                            Material = p.Material,
                            ProductoExistencia = p.ProductoExistencia.Select(pe => new ProductoExistencia()
                            {
                                ProductoExistenciaId = pe.ProductoExistenciaId,
                                ProductoId = pe.Producto.ProductoId,
                                Minimo = pe.Minimo,
                                Maximo = pe.Maximo,
                                Reservado = pe.Reservado,
                                Existente = pe.Existente,
                                FechaArribo = pe.FechaArribo
                            }).ToList(),
                            ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                            {
                                ProductoFotoId = pf.ProductoFotoId,
                                ProductoId = pf.ProductoFotoId,
                                UrlImagen = pf.UrlImagen
                            }).ToList()
                        }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var msj = ex;
                throw;
            }
            return resultado;
        }

        public override List<Producto> Obtener(int id, string anioId)
        {
            throw new NotImplementedException();
        }

        public override Resultado<Producto> Agregar(Producto modelo)
        {
            Resultado<Producto> resultado = new Resultado<Producto>();

            WebApi.Producto productoNuevo = new WebApi.Producto();

            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    ObjectExtensions.CopyObject(productoNuevo, modelo);
                    contexto.Producto.Add(productoNuevo);
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

        public override Resultado<Producto> Editar(Producto modelo)
        {
            Resultado<Producto> resultado = new Resultado<Producto>();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    var productoEncontrado = contexto.Producto.Where(p => p.ProductoId == modelo.ProductoId).FirstOrDefault();
                    //contexto.Entry(modelo).State = EntityState.Modified;
                    productoEncontrado.CategoriaId = modelo.CategoriaId;
                    productoEncontrado.Codigo = modelo.Codigo;
                    productoEncontrado.CodigoBarra = modelo.CodigoBarra;
                    productoEncontrado.Nombre = modelo.Nombre;
                    productoEncontrado.Pieza = modelo.Pieza;
                    productoEncontrado.Descripcion = modelo.Descripcion;
                    productoEncontrado.Material = modelo.Material;
                    productoEncontrado.Color = modelo.Color;
                    productoEncontrado.Capacidad = modelo.Capacidad;
                    productoEncontrado.Medida = modelo.Medida;
                    productoEncontrado.EsPrincipal = modelo.EsPrincipal;
                    productoEncontrado.CodigoPrincipal = modelo.CodigoPrincipal;
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

        public Resultado<Producto> EditarEstatusProducto(Producto modelo)
        {
            Resultado<Producto> resultado = new Resultado<Producto>();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    var productoEncontrado = contexto.Producto.Where(p => p.ProductoId == modelo.ProductoId).FirstOrDefault();
                    productoEncontrado.Activo = !productoEncontrado.Activo;
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

        public Resultado<ProductoExistencia> EditarProductoExistencia(ProductoExistencia modelo)
        {
            Resultado<ProductoExistencia> resultado = new Resultado<ProductoExistencia>();
            ProductoExistencia insertar = new ProductoExistencia();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    var res = contexto.ProductoExistencia.Where(p => p.ProductoExistenciaId == modelo.ProductoExistenciaId || p.ProductoId == modelo.ProductoId).FirstOrDefault();
                    if (res != null)
                    {
                        res.Minimo = modelo.Minimo;
                        res.Maximo = modelo.Maximo;
                        res.Reservado = modelo.Reservado;
                        res.Existente = modelo.Existente;
                        res.FechaArribo = modelo.FechaArribo;
                        contexto.SaveChanges();

                    }
                    else
                    {
                        //insertar.ProductoId = modelo.ProductoId;
                        //insertar.Minimo = modelo.Minimo;
                        //insertar.Maximo = modelo.Maximo;
                        //insertar.Existente = modelo.Existente;
                        //insertar.FechaArribo = modelo.FechaArribo;
                        WebApi.ProductoExistencia productoNuevo = new WebApi.ProductoExistencia();
                        ObjectExtensions.CopyObject(productoNuevo, modelo);
                        contexto.ProductoExistencia.Add(productoNuevo);
                        contexto.SaveChanges();
                    }

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

        public override Resultado<Producto> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        //Obtener los productos por categoria (8)
        public List<Producto> ObtenerTopProductosComprados()
        {
            List<Producto> resultado = new List<Producto>();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.Producto
                        .Select(p => new Producto()
                        {
                            ProductoId = p.ProductoId,
                            Codigo = p.Codigo,
                            CodigoBarra = p.CodigoBarra,
                            Nombre = p.Nombre,
                            Pieza = p.Pieza,
                            Material = p.Material,
                            Color = p.Color,
                            Capacidad = p.Capacidad,
                            Descripcion = p.Descripcion

                        })
                        .Take(16)
                        .OrderByDescending(x => x.ProductoId).ToList();

                }

            }
            catch (Exception ex)
            {
                var msj = ex;
                throw;
            }
            return resultado;
        }

        //Obtener los ultimos productos registrados (12)
        public List<Producto> ObtenerNuevosProductos(string _orderBy)
        {
            List<Producto> resultado = new List<Producto>();
            var orderBy = "ProductoId";
            switch (_orderBy)
            {
                case "asc":
                    orderBy = "Descripcion ASC";
                    break;
                case "desc":
                    orderBy = "Descripcion DESC";
                    break;
                case "mencapacidad":
                    orderBy = "Capacidad ASC";
                    break;
                case "maycapacidad":
                    orderBy = "Capacidad DESC";
                    break;
                default:
                    orderBy = "ProductoId";
                    break;
            }
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.Producto
                    .Where(p => p.EsPrincipal == true)
                    .Select(p => new Producto()
                    {
                        ProductoId = p.ProductoId,
                        Categoria = new Categoria()
                        {
                            CategoriaId = p.Categoria.CategoriaId,
                            Descripcion = p.Categoria.Descripcion,
                            TiempoBanner = p.Categoria.TiempoBanner
                        },
                        Codigo = p.Codigo,
                        CodigoBarra = p.CodigoBarra,
                        Nombre = p.Nombre,
                        Pieza = p.Pieza,
                        Material = p.Material,
                        Color = p.Color,
                        Capacidad = p.Capacidad,
                        Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                        Precio = p.Precio.Select(pr => new Precio()
                        {
                            PrecioId = pr.PrecioId,
                            ProductoId = pr.Producto.ProductoId,
                            Precio1 = pr.Precio1,
                            Precio2 = pr.Precio2,
                            Precio3 = pr.Precio3,
                            Mes = pr.Mes,
                            ayo = pr.ayo,
                            Emitio = pr.Emitio
                        }).ToList(),
                        ProductoExistencia = p.ProductoExistencia.Select(pe => new ProductoExistencia()
                        {
                            ProductoExistenciaId = pe.ProductoExistenciaId,
                            ProductoId = pe.Producto.ProductoId,
                            Minimo = pe.Minimo,
                            Maximo = pe.Maximo,
                            Existente = pe.Existente,
                            FechaArribo = pe.FechaArribo
                        }).ToList(),
                        ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                        {
                            ProductoFotoId = pf.ProductoFotoId,
                            ProductoId = pf.ProductoFotoId,
                            UrlImagen = pf.UrlImagen
                        }).ToList()

                    })
                    .OrderBy("ProductoId DESC")
                    .Take(12)                    
                    .OrderBy(orderBy)
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

        //Obtener los productos por categoria (12)
        //        CategoriaId Descripcion
        //1	Memorias USB
        //2	Power Bank
        //3	Audio
        //4	Tecnología
        //5	Varios
        public List<Producto> ObtenerProductosCategoria(int categoriaId)
        {
            List<Producto> resultado = new List<Producto>();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.Producto
                        .Where(p => p.Categoria.CategoriaId == categoriaId)
                        .Select(p => new Producto()
                        {
                            ProductoId = p.ProductoId,
                            Categoria = new Categoria()
                            {
                                CategoriaId = p.Categoria.CategoriaId,
                                Descripcion = p.Categoria.Descripcion,
                                TiempoBanner = p.Categoria.TiempoBanner
                            },
                            Codigo = p.Codigo,
                            CodigoBarra = p.CodigoBarra,
                            Nombre = p.Nombre,
                            Pieza = p.Pieza,
                            Material = p.Material,
                            Color = p.Color,
                            Capacidad = p.Capacidad,
                            Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                            Precio = p.Precio.Select(pr => new Precio()
                            {
                                PrecioId = pr.PrecioId,
                                ProductoId = pr.ProductoId,
                                Precio1 = pr.Precio1,
                                Precio2 = pr.Precio2,
                                Precio3 = pr.Precio3,
                                Mes = pr.Mes,
                                ayo = pr.ayo,
                                Emitio = pr.Emitio
                            }).ToList(),
                            ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                            {
                                ProductoFotoId = pf.ProductoFotoId,
                                ProductoId = pf.ProductoFotoId,
                                UrlImagen = pf.UrlImagen
                            }).ToList()
                        })
                        .Take(12).ToList();
                    //foreach (var item in resultado)
                    //{
                    //    if (item.Descripcion.Length > 30) item.DescripcionCorto = item.Descripcion.Substring(0, 29) + "...";
                    //    else item.DescripcionCorto = item.Descripcion+"\n";
                    //}
                }
            }
            catch (Exception ex)
            {
                var msj = ex;
                throw;
            }
            return resultado;
        }

        public List<Producto> ObtenerProductosCategoriaPaginado(int categoriaId, string _orderBy)
        {
            List<Producto> resultado, items = new List<Producto>();
            var orderBy = "ProductoId";
            //var tipoPrecio = "Precio2";
            //asc
            //desc
            //mencapacidad
            //maycapacidad
            //
            switch (_orderBy)
            {
                case "asc":
                    orderBy = "Descripcion ASC";
                    break;
                case "desc":
                    orderBy = "Descripcion DESC";
                    break;
                case "mencapacidad":
                    orderBy = "Capacidad ASC";
                    break;
                case "maycapacidad":
                    orderBy = "Capacidad DESC";
                    break;
                default:
                    orderBy = "ProductoId";
                    break;
            }
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {

                    resultado = contexto.Producto
                        .Where(p => p.Categoria.CategoriaId == categoriaId && p.EsPrincipal == true)
                        .Select(p => new Producto()
                        {
                            ProductoId = p.ProductoId,
                            Categoria = new Categoria()
                            {
                                CategoriaId = p.Categoria.CategoriaId,
                                Descripcion = p.Categoria.Descripcion,
                                TiempoBanner = p.Categoria.TiempoBanner
                            },
                            Codigo = p.Codigo,
                            CodigoBarra = p.CodigoBarra,
                            Nombre = p.Nombre,
                            Pieza = p.Pieza,
                            Material = p.Material,
                            Color = p.Color,
                            Capacidad = p.Capacidad,
                            EsPrincipal = p.EsPrincipal,
                            Medida = p.Medida,
                            CodigoPrincipal = p.CodigoPrincipal,
                            Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                            Precio = p.Precio.Select(pr => new Precio()
                            {
                                PrecioId = pr.PrecioId,
                                ProductoId = pr.ProductoId,
                                //PrecioUsuario = tipoPrecio == "Precio1" ? (double)pr.Precio1 : tipoPrecio == "Precio2" ? (double)pr.Precio2 : tipoPrecio == "Precio3" ? (double)pr.Precio3 : 0,
                                Precio1 = pr.Precio1,
                                Precio2 = pr.Precio2,
                                Precio3 = pr.Precio3,
                                Mes = pr.Mes,
                                ayo = pr.ayo,
                                Emitio = pr.Emitio
                            })
                            .ToList(),
                            ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                            {
                                ProductoFotoId = pf.ProductoFotoId,
                                ProductoId = pf.ProductoId,
                                UrlImagen = pf.UrlImagen
                            }).ToList()

                        })
                        .OrderBy(orderBy)
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

        public List<Producto> BuscadorProductosCategoria(int categoriaId, string descripcion, string color, string capacidad, string material, string _orderBy)
        {
            //List<BuscarProductos> resultado = new List<BuscarProductos>();
            //try
            //{
            //    using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
            //    {
            //        var resSP = contexto.BuscadorProductos(categoriaId, descripcion, color, capacidad, material);

            //        foreach (var item in resSP)
            //        {
            //            BuscarProductos entidad = new BuscarProductos
            //            {
            //                ProductoId = item.ProductoId,
            //                CategoriaId = item.CategoriaId,
            //                Codigo = item.Codigo,
            //                CodigoBarra = item.CodigoBarra,
            //                Nombre = item.Nombre,
            //                Pieza = item.Pieza,
            //                Descripcion = item.Descripcion,
            //                Color = item.Color,
            //                Capacidad = item.Capacidad,
            //                Material = item.Material

            //            };
            //            resultado.Add(entidad);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var msj = ex;
            //}
            //return resultado;
            var orderBy = "ProductoId";
            switch (_orderBy)
            {
                case "asc":
                    orderBy = "Descripcion ASC";
                    break;
                case "desc":
                    orderBy = "Descripcion DESC";
                    break;
                case "mencapacidad":
                    orderBy = "Capacidad ASC";
                    break;
                case "maycapacidad":
                    orderBy = "Capacidad DESC";
                    break;
                default:
                    orderBy = "ProductoId";
                    break;
            }

            List<Producto> resultado = new List<Producto>();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    if (color != "null" && capacidad != "null" && material != "null")
                    {
                        resultado = contexto.Producto
                        .Where(p => p.Categoria.CategoriaId == categoriaId && p.Descripcion.Contains(descripcion) && p.Color.Contains(color) && p.Capacidad.Contains(capacidad) && p.Material.Contains(material))
                        .Select(p => new Producto()
                        {
                            ProductoId = p.ProductoId,
                            Categoria = new Categoria()
                            {
                                CategoriaId = p.Categoria.CategoriaId,
                                Descripcion = p.Categoria.Descripcion,
                                TiempoBanner = p.Categoria.TiempoBanner
                            },
                            Codigo = p.Codigo,
                            CodigoBarra = p.CodigoBarra,
                            Nombre = p.Nombre,
                            Pieza = p.Pieza,
                            Material = p.Material,
                            Color = p.Color,
                            Capacidad = p.Capacidad,
                            Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                            Precio = p.Precio.Select(pr => new Precio()
                            {
                                PrecioId = pr.PrecioId,
                                ProductoId = pr.ProductoId,
                                Precio1 = pr.Precio1,
                                Precio2 = pr.Precio2,
                                Precio3 = pr.Precio3,
                                Mes = pr.Mes,
                                ayo = pr.ayo,
                                Emitio = pr.Emitio
                            }).ToList(),
                            ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                            {
                                ProductoFotoId = pf.ProductoFotoId,
                                ProductoId = pf.ProductoFotoId,
                                UrlImagen = pf.UrlImagen
                            }).ToList()

                        }).OrderBy(orderBy).ToList();
                    }

                    if (color == "null" && capacidad != "null" && material != "null")
                    {
                        resultado = contexto.Producto
                            .Where(p => p.Categoria.CategoriaId == categoriaId && p.Descripcion.Contains(descripcion) && p.Capacidad.Contains(capacidad) && p.Material.Contains(material))
                            .Select(p => new Producto()
                            {
                                ProductoId = p.ProductoId,
                                Categoria = new Categoria()
                                {
                                    CategoriaId = p.Categoria.CategoriaId,
                                    Descripcion = p.Categoria.Descripcion,
                                    TiempoBanner = p.Categoria.TiempoBanner
                                },
                                Codigo = p.Codigo,
                                CodigoBarra = p.CodigoBarra,
                                Nombre = p.Nombre,
                                Pieza = p.Pieza,
                                Material = p.Material,
                                Color = p.Color,
                                Capacidad = p.Capacidad,
                                Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                                Precio = p.Precio.Select(pr => new Precio()
                                {
                                    PrecioId = pr.PrecioId,
                                    ProductoId = pr.ProductoId,
                                    Precio1 = pr.Precio1,
                                    Precio2 = pr.Precio2,
                                    Precio3 = pr.Precio3,
                                    Mes = pr.Mes,
                                    ayo = pr.ayo,
                                    Emitio = pr.Emitio
                                }).ToList(),
                                ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                                {
                                    ProductoFotoId = pf.ProductoFotoId,
                                    ProductoId = pf.ProductoFotoId,
                                    UrlImagen = pf.UrlImagen
                                }).ToList()

                            }).OrderBy(orderBy).ToList();
                    }

                    if (color == "null" && capacidad == "null" && material != "null")
                    {
                        resultado = contexto.Producto
                            .Where(p => p.Categoria.CategoriaId == categoriaId && p.Descripcion.Contains(descripcion) && p.Material.Contains(material))
                            .Select(p => new Producto()
                            {
                                ProductoId = p.ProductoId,
                                Categoria = new Categoria()
                                {
                                    CategoriaId = p.Categoria.CategoriaId,
                                    Descripcion = p.Categoria.Descripcion,
                                    TiempoBanner = p.Categoria.TiempoBanner
                                },
                                Codigo = p.Codigo,
                                CodigoBarra = p.CodigoBarra,
                                Nombre = p.Nombre,
                                Pieza = p.Pieza,
                                Material = p.Material,
                                Color = p.Color,
                                Capacidad = p.Capacidad,
                                Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                                Precio = p.Precio.Select(pr => new Precio()
                                {
                                    PrecioId = pr.PrecioId,
                                    ProductoId = pr.ProductoId,
                                    Precio1 = pr.Precio1,
                                    Precio2 = pr.Precio2,
                                    Precio3 = pr.Precio3,
                                    Mes = pr.Mes,
                                    ayo = pr.ayo,
                                    Emitio = pr.Emitio
                                }).ToList(),
                                ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                                {
                                    ProductoFotoId = pf.ProductoFotoId,
                                    ProductoId = pf.ProductoFotoId,
                                    UrlImagen = pf.UrlImagen
                                }).ToList()

                            }).OrderBy(orderBy).ToList();
                    }

                    if (color != "null" && capacidad != "null" && material == "null")
                    {
                        resultado = contexto.Producto
                            .Where(p => p.Categoria.CategoriaId == categoriaId && p.Descripcion.Contains(descripcion) && p.Color.Contains(color) && p.Capacidad.Contains(capacidad))
                            .Select(p => new Producto()
                            {
                                ProductoId = p.ProductoId,
                                Categoria = new Categoria()
                                {
                                    CategoriaId = p.Categoria.CategoriaId,
                                    Descripcion = p.Categoria.Descripcion,
                                    TiempoBanner = p.Categoria.TiempoBanner
                                },
                                Codigo = p.Codigo,
                                CodigoBarra = p.CodigoBarra,
                                Nombre = p.Nombre,
                                Pieza = p.Pieza,
                                Material = p.Material,
                                Color = p.Color,
                                Capacidad = p.Capacidad,
                                Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                                Precio = p.Precio.Select(pr => new Precio()
                                {
                                    PrecioId = pr.PrecioId,
                                    ProductoId = pr.ProductoId,
                                    Precio1 = pr.Precio1,
                                    Precio2 = pr.Precio2,
                                    Precio3 = pr.Precio3,
                                    Mes = pr.Mes,
                                    ayo = pr.ayo,
                                    Emitio = pr.Emitio
                                }).ToList(),
                                ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                                {
                                    ProductoFotoId = pf.ProductoFotoId,
                                    ProductoId = pf.ProductoFotoId,
                                    UrlImagen = pf.UrlImagen
                                }).ToList()

                            }).OrderBy(orderBy).ToList();
                    }

                    if (color != "null" && capacidad == "null" && material != "null")
                    {
                        resultado = contexto.Producto
                            .Where(p => p.Categoria.CategoriaId == categoriaId && p.Descripcion.Contains(descripcion) && p.Color.Contains(color) && p.Material.Contains(material))
                            .Select(p => new Producto()
                            {
                                ProductoId = p.ProductoId,
                                Categoria = new Categoria()
                                {
                                    CategoriaId = p.Categoria.CategoriaId,
                                    Descripcion = p.Categoria.Descripcion,
                                    TiempoBanner = p.Categoria.TiempoBanner
                                },
                                Codigo = p.Codigo,
                                CodigoBarra = p.CodigoBarra,
                                Nombre = p.Nombre,
                                Pieza = p.Pieza,
                                Material = p.Material,
                                Color = p.Color,
                                Capacidad = p.Capacidad,
                                Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                                Precio = p.Precio.Select(pr => new Precio()
                                {
                                    PrecioId = pr.PrecioId,
                                    ProductoId = pr.ProductoId,
                                    Precio1 = pr.Precio1,
                                    Precio2 = pr.Precio2,
                                    Precio3 = pr.Precio3,
                                    Mes = pr.Mes,
                                    ayo = pr.ayo,
                                    Emitio = pr.Emitio
                                }).ToList(),
                                ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                                {
                                    ProductoFotoId = pf.ProductoFotoId,
                                    ProductoId = pf.ProductoFotoId,
                                    UrlImagen = pf.UrlImagen
                                }).ToList()

                            }).OrderBy(orderBy).ToList();
                    }

                    if (color != "null" && capacidad == "null" && material == "null")
                    {
                        resultado = contexto.Producto
                            .Where(p => p.Categoria.CategoriaId == categoriaId && p.Descripcion.Contains(descripcion) && p.Color.Contains(color))
                            .Select(p => new Producto()
                            {
                                ProductoId = p.ProductoId,
                                Categoria = new Categoria()
                                {
                                    CategoriaId = p.Categoria.CategoriaId,
                                    Descripcion = p.Categoria.Descripcion,
                                    TiempoBanner = p.Categoria.TiempoBanner
                                },
                                Codigo = p.Codigo,
                                CodigoBarra = p.CodigoBarra,
                                Nombre = p.Nombre,
                                Pieza = p.Pieza,
                                Material = p.Material,
                                Color = p.Color,
                                Capacidad = p.Capacidad,
                                Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                                Precio = p.Precio.Select(pr => new Precio()
                                {
                                    PrecioId = pr.PrecioId,
                                    ProductoId = pr.ProductoId,
                                    Precio1 = pr.Precio1,
                                    Precio2 = pr.Precio2,
                                    Precio3 = pr.Precio3,
                                    Mes = pr.Mes,
                                    ayo = pr.ayo,
                                    Emitio = pr.Emitio
                                }).ToList(),
                                ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                                {
                                    ProductoFotoId = pf.ProductoFotoId,
                                    ProductoId = pf.ProductoFotoId,
                                    UrlImagen = pf.UrlImagen
                                }).ToList()

                            }).OrderBy(orderBy).ToList();
                    }

                    if (color == "null" && capacidad != "null" && material == "null")
                    {
                        resultado = contexto.Producto
                            .Where(p => p.Categoria.CategoriaId == categoriaId && p.Descripcion.Contains(descripcion) && p.Capacidad.Contains(capacidad))
                            .Select(p => new Producto()
                            {
                                ProductoId = p.ProductoId,
                                Categoria = new Categoria()
                                {
                                    CategoriaId = p.Categoria.CategoriaId,
                                    Descripcion = p.Categoria.Descripcion,
                                    TiempoBanner = p.Categoria.TiempoBanner
                                },
                                Codigo = p.Codigo,
                                CodigoBarra = p.CodigoBarra,
                                Nombre = p.Nombre,
                                Pieza = p.Pieza,
                                Material = p.Material,
                                Color = p.Color,
                                Capacidad = p.Capacidad,
                                Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                                Precio = p.Precio.Select(pr => new Precio()
                                {
                                    PrecioId = pr.PrecioId,
                                    ProductoId = pr.ProductoId,
                                    Precio1 = pr.Precio1,
                                    Precio2 = pr.Precio2,
                                    Precio3 = pr.Precio3,
                                    Mes = pr.Mes,
                                    ayo = pr.ayo,
                                    Emitio = pr.Emitio
                                }).ToList(),
                                ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                                {
                                    ProductoFotoId = pf.ProductoFotoId,
                                    ProductoId = pf.ProductoFotoId,
                                    UrlImagen = pf.UrlImagen
                                }).ToList()

                            }).OrderBy(orderBy).ToList();
                    }

                    if (color == "null" && capacidad == "null" && material == "null")
                    {
                        resultado = contexto.Producto
                            .Where(p => p.Categoria.CategoriaId == categoriaId && p.Descripcion.Contains(descripcion))
                            .Select(p => new Producto()
                            {
                                ProductoId = p.ProductoId,
                                Categoria = new Categoria()
                                {
                                    CategoriaId = p.Categoria.CategoriaId,
                                    Descripcion = p.Categoria.Descripcion,
                                    TiempoBanner = p.Categoria.TiempoBanner
                                },
                                Codigo = p.Codigo,
                                CodigoBarra = p.CodigoBarra,
                                Nombre = p.Nombre,
                                Pieza = p.Pieza,
                                Material = p.Material,
                                Color = p.Color,
                                Capacidad = p.Capacidad,
                                Descripcion = p.Descripcion.Length > 25 ? p.Descripcion.Substring(0, 24) + "..." : p.Descripcion,
                                Precio = p.Precio.Select(pr => new Precio()
                                {
                                    PrecioId = pr.PrecioId,
                                    ProductoId = pr.ProductoId,
                                    Precio1 = pr.Precio1,
                                    Precio2 = pr.Precio2,
                                    Precio3 = pr.Precio3,
                                    Mes = pr.Mes,
                                    ayo = pr.ayo,
                                    Emitio = pr.Emitio
                                }).ToList(),
                                ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                                {
                                    ProductoFotoId = pf.ProductoFotoId,
                                    ProductoId = pf.ProductoFotoId,
                                    UrlImagen = pf.UrlImagen
                                }).ToList()

                            }).OrderBy(orderBy)
                            .ToList();
                    }
                }
                
            }
            catch (Exception ex)
            {
                var msj = ex;
                throw;
            }
            return resultado;
        }

        public List<Producto> ObtenerProductosPorCodigo(string codigo)
        {
            List<Producto> resultado = new List<Producto>();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.Producto
                        .Where(x => x.CodigoPrincipal == codigo && x.EsPrincipal == false)
                        .Select(p => new Producto()
                        {
                            ProductoId = p.ProductoId,
                            Codigo = p.Codigo,
                            CodigoBarra = p.CodigoBarra,
                            Nombre = p.Nombre,
                            Pieza = p.Pieza,
                            Descripcion = p.Descripcion,
                            EsPrincipal = p.EsPrincipal,
                            Medida = p.Medida,
                            CodigoPrincipal = p.CodigoPrincipal,
                            Precio = p.Precio.Select(pr => new Precio()
                            {
                                PrecioId = pr.PrecioId,
                                ProductoId = pr.ProductoId,
                                Precio1 = pr.Precio1,
                                Precio2 = pr.Precio2,
                                Precio3 = pr.Precio3,
                                Mes = pr.Mes,
                                ayo = pr.ayo,
                                Emitio = pr.Emitio
                            }).ToList(),
                            Color = p.Color,
                            Capacidad = p.Capacidad,
                            Material = p.Material
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                var msj = ex;
                throw;
            }
            return resultado;
        }

        public Producto ObtenerProductoDetallePorCodigo(string codigo)
        {
            Producto resultado = new Producto();
            try
            {
                using (var contexto = new WebApi.ControlAlmacenDigitalEntities())
                {
                    resultado = contexto.Producto
                        .Where(x => x.Codigo == codigo)
                        .Select(p => new Producto()
                        {
                            ProductoId = p.ProductoId,
                            Codigo = p.Codigo,
                            CodigoBarra = p.CodigoBarra,
                            Nombre = p.Nombre,
                            Pieza = p.Pieza,
                            Descripcion = p.Descripcion,
                            EsPrincipal = p.EsPrincipal,
                            Medida = p.Medida,
                            CodigoPrincipal = p.CodigoPrincipal,
                            Color = p.Color,
                            Capacidad = p.Capacidad,
                            Material = p.Material,
                            ProductoFotografia = p.ProductoFotografia.Select(pf => new ProductoFotografia()
                            {
                                ProductoFotoId = pf.ProductoFotoId,
                                ProductoId = pf.ProductoFotoId,
                                UrlImagen = pf.UrlImagen
                            }).ToList(),
                        }).FirstOrDefault();
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