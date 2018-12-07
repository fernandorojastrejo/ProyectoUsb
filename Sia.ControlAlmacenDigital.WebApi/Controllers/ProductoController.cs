using Newtonsoft.Json;
using Sia.ControlAlmacenDigital.Entidades;
using Sia.ControlAlmacenDigital.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Sia.ControlAlmacenDigital.Controllers
{
    [RoutePrefix("api/Producto")]
    public class ProductoController : ApiController
    {
        [Route("")]
        public IEnumerable<Producto> Get()
        {
            ProductoNeg negocio = new ProductoNeg();
            var resultado = negocio.Obtener();
            return resultado;
        }

        [Route("ObtenerTopProductosComprados")]
        public IEnumerable<Producto> GetObtenerTopProductosComprados()
        {
            ProductoNeg negocio = new ProductoNeg();
            var resultado = negocio.ObtenerTopProductosComprados();
            return resultado;
        }

        [Route("ObtenerDetalleProducto/{id}")]
        public Producto GetObtenerDetalleProducto(int id)
        {
            ProductoNeg negocio = new ProductoNeg();
            var resultado = negocio.Obtener(id);
            return resultado;
        }

        [Route("ObtenerNuevosProductos/{orderBy}")]
        public IEnumerable<Producto> GetObtenerNuevosProductos(string orderBy)
        {
            ProductoNeg negocio = new ProductoNeg();
            var resultado = negocio.ObtenerNuevosProductos(orderBy);
            return resultado;
        }

        [Route("ObtenerProductosCategoria/{id}")]
        public IEnumerable<Producto> GetObtenerProductosCategoria(int id)
        {
            ProductoNeg negocio = new ProductoNeg();
            var resultado = negocio.ObtenerProductosCategoria(id);
            return resultado;
        }

        [Route("AgregarProducto")]
        public Resultado<Producto> AgregarProducto(Producto modelo)
        {
            ProductoNeg neg = new ProductoNeg();
            return neg.Agregar(modelo);
        }

        [HttpPut]
        [Route("EditarProducto")]
        public Resultado<Producto> EditarProducto(Producto modelo)
        {
            ProductoNeg neg = new ProductoNeg();
            return neg.Editar(modelo);
        }

        [HttpPut]
        [Route("EditarEstatusProducto")]
        public Resultado<Producto> EditarEstatusProducto(Producto modelo)
        {
            ProductoNeg neg = new ProductoNeg();
            return neg.EditarEstatusProducto(modelo);
        }

        
        [Route("EditarProductoExistencia")]
        public Resultado<ProductoExistencia> EditarProductoExistencia(ProductoExistencia modelo)
        {
            ProductoNeg neg = new ProductoNeg();
            return neg.EditarProductoExistencia(modelo);
        }

        [HttpPost]
        [Route("SubirImagenProducto")]
        public Resultado<ProductoFotografia> SubirImagenProducto()
        {
            Resultado<ProductoFotografia> resultado = new Resultado<ProductoFotografia>();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            resultado.Error = true;
                            resultado.MensajeError = WebApi.Recursos.ErrorFormatoFoto;
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format(" Peso máximo de la imagen 1 mb.");
                            resultado.Error = true;
                            resultado.MensajeError = WebApi.Recursos.ErrorPesoMaxFoto;
                        }
                        else
                        {



                            var filePath = HttpContext.Current.Server.MapPath("~/Images/Productos/" + postedFile.FileName);

                            postedFile.SaveAs(filePath);

                        }
                    }
                    resultado.Exito = true;
                    resultado.MensajeExito = WebApi.Recursos.ExitoAgregarFoto;
                }
                var res = string.Format(" Seleccione una imagen.");
                resultado.Error = true;
                resultado.MensajeError = WebApi.Recursos.ErrorAgregar + res;
            }
            catch (Exception ex)
            {
                resultado.Error = true;
                resultado.MensajeError = WebApi.Recursos.ErrorAgregarFoto + ex;
            }
            return resultado;
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET", exposedHeaders: "Paging-Headers")]
        [Route("ObtenerProductosCategoriaPaginado/{id}/{pageNumber}/{orderBy}")]
        public IEnumerable<Producto> GetObtenerProductosCategoriaPaginado(int id, int pageNumber, string orderBy)
        {
            ProductoNeg negocio = new ProductoNeg();
            List<Producto> items = new List<Producto>();

            var count = negocio.ObtenerProductosCategoriaPaginado(id, orderBy).Count();
            var resultado = negocio.ObtenerProductosCategoriaPaginado(id, orderBy);
            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = 12;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            items = resultado.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            // Setting Header  
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            return items;
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET", exposedHeaders: "Paging-Headers")]
        [Route("BuscarProductosCategoria/{categoriaId}/{descripcion}/{color}/{capacidad}/{material}/{pageNumber}/{orderBy}")]
        public IEnumerable<Producto> GetBuscarProductosCategoria(int categoriaId, string descripcion, string color, string capacidad, string material, int pageNumber, string orderBy)
        {
            //ProductoNeg negocio = new ProductoNeg();
            //var resultado = negocio.BuscadorProductosCategoria(categoriaId, descripcion, color, capacidad, material);
            //return resultado;
            ProductoNeg negocio = new ProductoNeg();
            List<Producto> items = new List<Producto>();

            var count = negocio.BuscadorProductosCategoria(categoriaId, descripcion, color, capacidad, material, orderBy).Count();
            var resultado = negocio.BuscadorProductosCategoria(categoriaId, descripcion, color, capacidad, material, orderBy);
            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = 12;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            items = resultado.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            // Setting Header  
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            return items;
        }

        [Route("ObtenerProductosPorCodigo/{codigo}")]
        public List<Producto> GetObtenerDetalleCodigoProducto(string codigo)
        {
            ProductoNeg negocio = new ProductoNeg();
            var resultado = negocio.ObtenerProductosPorCodigo(codigo);
            return resultado;
        }

        [Route("ObtenerProductoDetallePorCodigo/{codigo}")]
        public Producto GetObtenerProductoDetallePorCodigo(string codigo)
        {
            ProductoNeg negocio = new ProductoNeg();
            var resultado = negocio.ObtenerProductoDetallePorCodigo(codigo);
            return resultado;
        }

    }
}
