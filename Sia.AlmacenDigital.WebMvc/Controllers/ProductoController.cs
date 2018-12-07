using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Sia.AlmacenDigital.WebMvc.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
  
        // GET: Producto
        public ActionResult UploadedFile(int productID, int categoID, string webApi)
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
                    switch (categoID)
                    {
                        case 1:
                            folderCategoria = "memoriasUsb";
                            break;
                        case 2:
                            folderCategoria = "powerBank";
                            break;
                        case 3:
                            folderCategoria = "audio";
                            break;
                        case 4:
                            folderCategoria = "tecnologia";
                            break;
                        case 5:
                            folderCategoria = "varios";
                            break;
                        case 6:
                            folderCategoria = "principal";
                            break;
                        case 7:
                            folderCategoria = "promocion";
                            break;
                        default:
                            break;
                    }
                    //if (categoID.Equals(3)) folderCategoria = "audio";
                    //else if (categoID.Equals(1)) folderCategoria = "memoriasUsb";
                    //else if (categoID.Equals(2)) folderCategoria = "powerBank";
                    //else if (categoID.Equals(4)) folderCategoria = "tecnologia";
                    //else folderCategoria = "varios";
                    pathString = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Images\\categoria\\" + folderCategoria;
                    file.SaveAs(pathString + "\\" + file.FileName);
                    esArchivoValido = true;
                    resultado = "ok";
                }
            }
         
          
            if (esArchivoValido)
            {
                resultado = "ok";
                //var data = new
                //{
                //    ProductoId = productID,
                //    UrlImagen = "Images/categoria/" + folderCategoria + "/" + fName
                //};

                //using (var client = new HttpClient())
                //{
                //    try
                //    {
                //        client.BaseAddress = new Uri(webApi);
                //        client.DefaultRequestHeaders.Accept.Clear();
                //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //        //string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                //        //HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                //        //HttpResponseMessage response = client.PostAsync("api/ProductoFotografia/AgregarProductoFotografia", content).Result;
                //        var response = await client.PostAsJsonAsync("api/ProductoFotografia/AgregarProductoFotografia", data);

                //        if (response.IsSuccessStatusCode)
                //        {
                //            //string res = response.Content.ReadAsAsync<string>().Result.ToString();
                //            //if (res.Equals("Exito")) 
                //            resultado = "ok";
                //            //else resultado = res;
                //        }
                //        else
                //        {
                //            resultado = "ErrorArchivo";
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        resultado =  e.Message.ToString();
                //    }
                //}
            }
            else resultado = "NoArchivo";
            return Json(new { Message = resultado });
        }
    }
}