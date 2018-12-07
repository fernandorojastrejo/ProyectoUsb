using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sia.AlmacenDigital.WebMvc.Controllers
{
    public class BannerController : Controller
    {
        // GET: Banner
        public ActionResult UploadedFileAsync(int categoID, string webApi)
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
                    //if (categoID.Equals(3)) folderCategoria = "audio";
                    //if (categoID.Equals(1)) folderCategoria = "memoriasUsb";
                    //if (categoID.Equals(2)) folderCategoria = "powerBank";
                    //if (categoID.Equals(4)) folderCategoria = "tecnologia";
                    //if (categoID.Equals(5)) folderCategoria = "varios";
                    //if (categoID.Equals(6)) folderCategoria = "principal";
                    //if (categoID.Equals(7)) folderCategoria = "promocion";
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
                    pathString = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Images\\banner\\" + folderCategoria;
                    file.SaveAs(pathString + "\\" + file.FileName);
                    esArchivoValido = true;
                }
            }


            if (esArchivoValido)
            {
                resultado = "ok";
                //var data = new
                //{
                //    CategoriaId = categoID,
                //    UrlImagen = "Images/banner/" + folderCategoria + "/" + fName
                //};

                //using (var client = new HttpClient())
                //{
                //    try
                //    {
                //        client.BaseAddress = new Uri(webApi);
                //        //client.DefaultRequestHeaders.Accept.Clear();
                //        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //        //string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                //        //HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                //        //HttpResponseMessage response = client.PostAsync("api/Banner/AgregarBanner", content).Result;
                //        var postTask = client.PostAsJsonAsync("api/Banner/AgregarBanner", data);
                //        postTask.Wait();
                //        var result = postTask.Result;
                //        if (result.IsSuccessStatusCode)
                //        {
                //            resultado = "ok";
                //        }
                //        else
                //        {
                //            resultado = "ErrorArchivo";
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        resultado = e.Message.ToString();
                //    }
                //}
            }
            else resultado = "NoArchivo";
            return Json(new { Message = resultado });
        }
    }
}