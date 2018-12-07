using Sia.ControlAlmacenDigital.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.IO;
using System.Text;
using System.Reflection;
namespace Sia.ControlAlmacenDigital.WebApi.Negocio
{
    public class ProductoExcelNeg
    {




        public Resultado<Entidades.Producto> AgregarEditarAtras(Entidades.Producto modelo)
        {
            Resultado<Entidades.Producto> resultado = new Resultado<Entidades.Producto>();
            ConectorSql conecta = new ConectorSql();
            //Objetos de para Excel
            OleDbConnection miConn = null;
            ConexionOle mioledb = new ConexionOle();
            DataTable dtExcelRecords = new DataTable();
        
            try
            {
                //string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + modelo.RutaExcel;
                string path = @"C:\inetpub\wwwroot\UsbTechnologyApi\" + modelo.RutaExcel;
                string cadenacon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='EXCEL 12.0;HDR=YES;IMEX=1'";
                miConn = mioledb.Abrir(cadenacon);
                dtExcelRecords = mioledb.Hoja(miConn, "Productos$", "", "", "", "");

                string[] rangos = mioledb.ObtieneRango(dtExcelRecords, 0, 0, 0);
                int Filainicial = Convert.ToInt32(rangos[0]);
                int Filafinal = Convert.ToInt32(rangos[3]);
                dtExcelRecords = mioledb.Hoja(miConn, "Productos$", "A", Filainicial.ToString(), "U", Filafinal.ToString());

                using (var contexto = new ControlAlmacenDigitalEntities())
                {
                    for (int i = 0; i < dtExcelRecords.Rows.Count; i++)
                    {
                        string CODIGO = dtExcelRecords.Rows[i][1].ToString().Trim(); if (CODIGO == null || CODIGO == "") CODIGO = "0";
                        if (CODIGO == "") break;
                        string CATEGORIA = dtExcelRecords.Rows[i][0].ToString().Trim();
                        string CODIGOBARRA = dtExcelRecords.Rows[i][2].ToString().Trim(); if (CODIGOBARRA == null || CODIGOBARRA == "") CODIGOBARRA = "0";
                        string NOMBRE = dtExcelRecords.Rows[i][3].ToString().Trim(); if (NOMBRE == null || NOMBRE == "") NOMBRE = "SIN NOMBRE";
                        string PIEZA = dtExcelRecords.Rows[i][4].ToString().Trim(); if (PIEZA == null || PIEZA == "") PIEZA = "0";
                        string DESCRIPCION = dtExcelRecords.Rows[i][5].ToString().Trim(); if (DESCRIPCION == null || DESCRIPCION == "") DESCRIPCION = "SIN DESCRIPCIÓN";
                        string COLOR = dtExcelRecords.Rows[i][6].ToString().Trim(); if (COLOR == null || COLOR == "") COLOR = "SIN COLOR";
                        string CAPACIDAD = dtExcelRecords.Rows[i][7].ToString().Trim(); if (CAPACIDAD == null || CAPACIDAD == "") CAPACIDAD = "0";
                        string MATERIAL = dtExcelRecords.Rows[i][8].ToString().Trim(); if (MATERIAL == null || MATERIAL == "") MATERIAL = "";
                        string ACTIVO = dtExcelRecords.Rows[i][9].ToString().Trim(); if (ACTIVO == null || ACTIVO == "") ACTIVO = "Inactivo";
                        bool isActivo = false;
                        if (ACTIVO.Equals("Activo")) isActivo = true;
                        string MINIMO = dtExcelRecords.Rows[i][10].ToString().Trim(); if (MINIMO == null || MINIMO == "") MINIMO = "0";
                        string MAXIMO = dtExcelRecords.Rows[i][11].ToString().Trim(); if (MAXIMO == null || MAXIMO == "") MAXIMO = "0";
                        string EXISTENTE = dtExcelRecords.Rows[i][12].ToString().Trim(); if (EXISTENTE == null || EXISTENTE == "") EXISTENTE = "0";
                        string RESERVADO = dtExcelRecords.Rows[i][13].ToString().Trim(); if (RESERVADO == null || RESERVADO == "") RESERVADO = "0";
                        string FECHAARRIVO = dtExcelRecords.Rows[i][14].ToString().Trim(); if (FECHAARRIVO == null || FECHAARRIVO == "") FECHAARRIVO = DateTime.Now.ToString();
                        string PRECIO1 = dtExcelRecords.Rows[i][15].ToString().Trim(); if (PRECIO1 == null || PRECIO1 == "") PRECIO1 = "0";
                        string PRECIO2 = dtExcelRecords.Rows[i][16].ToString().Trim(); if (PRECIO2 == null || PRECIO2 == "") PRECIO2 = "0";
                        string PRECIO3 = dtExcelRecords.Rows[i][17].ToString().Trim(); if (PRECIO3 == null || PRECIO3 == "") PRECIO3 = "0";
                        string MEDIDA = dtExcelRecords.Rows[i][18].ToString().Trim(); if (MEDIDA == null || MEDIDA == "") PRECIO3 = "0";
                        string ESPRINCIPAL = dtExcelRecords.Rows[i][19].ToString().Trim(); if (ESPRINCIPAL == null || ESPRINCIPAL == "") ESPRINCIPAL = "0";
                        bool isPrincipal = false;
                        if (ESPRINCIPAL.Equals("Sí")) isPrincipal = true;
                        string CODIGOPRINCIPAL = dtExcelRecords.Rows[i][20].ToString().Trim(); if (CODIGOPRINCIPAL == null || CODIGOPRINCIPAL == "") CODIGOPRINCIPAL = CODIGO;
                        string ProductoId = string.Empty;
                        var productoIdEncontrado = contexto.Producto.Where(m => m.Nombre == NOMBRE).FirstOrDefault();
                        var categoria = contexto.Categoria.Where(m => m.Descripcion == CATEGORIA).FirstOrDefault();
                        conecta.Abrirconexion();
                        conecta.comm = conecta.con.CreateCommand();
                        conecta.comm.CommandType = CommandType.StoredProcedure;

                        if (productoIdEncontrado == null)
                        {
                            conecta.comm.CommandText = "InsertaProducto";
                        }
                        else
                        {
                            conecta.comm.CommandText = "ActualizaProducto";
                            conecta.comm.Parameters.Add("@ProductoId", SqlDbType.NVarChar).Value = productoIdEncontrado.ProductoId;
                        }
                        conecta.comm.Parameters.Add("@CategoriaId", SqlDbType.Int).Value = categoria.CategoriaId;
                        conecta.comm.Parameters.Add("@Codigo", SqlDbType.NVarChar).Value = CODIGO;
                        conecta.comm.Parameters.Add("@CodigoBarra", SqlDbType.NVarChar).Value = CODIGOBARRA;
                        conecta.comm.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = NOMBRE;
                        conecta.comm.Parameters.Add("@Pieza", SqlDbType.Int).Value = PIEZA;
                        conecta.comm.Parameters.Add("@Descripcion", SqlDbType.NVarChar).Value = DESCRIPCION;
                        conecta.comm.Parameters.Add("@Color", SqlDbType.NVarChar).Value = COLOR;
                        conecta.comm.Parameters.Add("@Capacidad", SqlDbType.NVarChar).Value = CAPACIDAD;
                        conecta.comm.Parameters.Add("@Material", SqlDbType.NVarChar).Value = MATERIAL;
                        conecta.comm.Parameters.Add("@Activo", SqlDbType.Bit).Value = isActivo;
                        conecta.comm.Parameters.Add("@Medida", SqlDbType.NVarChar).Value = MEDIDA;
                        conecta.comm.Parameters.Add("@EsPrincipal", SqlDbType.Bit).Value = isPrincipal;
                        conecta.comm.Parameters.Add("@CodigoPrincipal", SqlDbType.NVarChar).Value = CODIGOPRINCIPAL;

                        if (productoIdEncontrado == null) conecta.comm.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        conecta.comm.ExecuteNonQuery();
                        if (productoIdEncontrado == null) ProductoId = conecta.comm.Parameters["@id"].Value.ToString();
                        conecta.CierraConexion();

                        //ProductoExistencia
                        conecta.Abrirconexion();
                        conecta.comm = conecta.con.CreateCommand();
                        conecta.comm.CommandType = CommandType.StoredProcedure;
                        if (productoIdEncontrado == null)
                        {
                            conecta.comm.CommandText = "InsertaProductoExistencia";
                            conecta.comm.Parameters.Add("@ProductoId", SqlDbType.Int).Value = ProductoId;
                        }
                        else
                        {
                            conecta.comm.CommandText = "ActualizaProductoExistencia";
                            conecta.comm.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoIdEncontrado.ProductoId;
                        }
                        
                        conecta.comm.Parameters.Add("@Minimo", SqlDbType.Int).Value = int.Parse(MINIMO);
                        conecta.comm.Parameters.Add("@Maximo", SqlDbType.Int).Value = int.Parse(MAXIMO);
                        conecta.comm.Parameters.Add("@Existente", SqlDbType.Int).Value = int.Parse(EXISTENTE);
                        conecta.comm.Parameters.Add("@Reservado", SqlDbType.Int).Value = int.Parse(RESERVADO);
                        conecta.comm.Parameters.Add("@FechaArribo", SqlDbType.DateTime).Value = DateTime.Parse(FECHAARRIVO);
                        conecta.comm.ExecuteNonQuery();
                        conecta.CierraConexion();

                        //Precio
                        conecta.Abrirconexion();
                        conecta.comm = conecta.con.CreateCommand();
                        conecta.comm.CommandType = CommandType.StoredProcedure;
                        if (productoIdEncontrado == null)
                        {
                            conecta.comm.CommandText = "InsertaProductoPrecio";
                            conecta.comm.Parameters.Add("@ProductoId", SqlDbType.Int).Value = ProductoId;
                        }
                        else
                        {
                            conecta.comm.CommandText = "ActualizaProductoPrecio";
                            conecta.comm.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoIdEncontrado.ProductoId;
                        }
                        conecta.comm.Parameters.Add("@Precio1", SqlDbType.Int).Value = float.Parse(PRECIO1);
                        conecta.comm.Parameters.Add("@Precio2", SqlDbType.Int).Value = float.Parse(PRECIO2);
                        conecta.comm.Parameters.Add("@Precio3", SqlDbType.Int).Value = float.Parse(PRECIO3);
                        conecta.comm.ExecuteNonQuery();
                        conecta.CierraConexion();

                        resultado.Exito = true;
                        resultado.MensajeExito = "Se cargo correctamente su información de productos.";
                    }
                    mioledb.Cierra(miConn);                   
                }
            }
            catch (Exception ex)
            {
                resultado.Error = true;
                resultado.MensajeError = ex.Message;
                mioledb.Cierra(miConn);
                
            }
            return resultado;
        }
        public Resultado<Entidades.Producto> AgregarEditar(Entidades.Producto modelo)
        {
            Resultado<Entidades.Producto> resultado = new Resultado<Entidades.Producto>();
            ConectorSql conecta = new ConectorSql();
            //Objetos de para Excel
            //OleDbConnection miConn = null;
            //ConexionOle mioledb = new ConexionOle();
            //DataTable dtExcelRecords = new DataTable();
            string path = @"C:\inetpub\wwwroot\UsbTechnologyApi\" + modelo.RutaExcel;
            try
            {


                //Declaro las variables necesarias
                Microsoft.Office.Interop.Excel._Application xlApp;
                Microsoft.Office.Interop.Excel._Workbook xlLibro;
                Microsoft.Office.Interop.Excel._Worksheet xlHoja1;
                Microsoft.Office.Interop.Excel.Sheets xlHojas;
                //asigno la ruta dónde se encuentra el archivo
                string fileName = path;
                //inicializo la variable xlApp (referente a la aplicación)
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                //Muestra la aplicación Excel si está en true
                xlApp.Visible = false;
                //Abrimos el libro a leer (documento excel)
                xlLibro = xlApp.Workbooks.Open(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Console.WriteLine("Abierto el libro excel");
                // Asignamos las hojas
                xlHojas = xlLibro.Sheets;
                //Asignamos la hoja con la que queremos trabajar: 
                xlHoja1 = (Microsoft.Office.Interop.Excel._Worksheet)xlHojas["Productos"];
                Console.WriteLine("Pasar a la hoja de productos");
                int j = 2;
                // recorremos las celdas que queremos y sacamos los datos 
                //10 es el número de filas que queremos que lea
                string consulta = "";
                bool existeReg = false;
                int CONTADORRM = 0;
                using (var contexto = new ControlAlmacenDigitalEntities())
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        string CODIGO = (string)xlHoja1.get_Range("B" + j, Missing.Value).Text;
                        if (CODIGO == "") break;
                        if (CODIGO == null || CODIGO == "") CODIGO = "0";
                        
                        string CATEGORIA = (string)xlHoja1.get_Range("A" + j, Missing.Value).Text;

                     
                        string CODIGOBARRA = (string)xlHoja1.get_Range("C" + j, Missing.Value).Text; if (CODIGOBARRA == null || CODIGOBARRA == "") CODIGOBARRA = "0";
                        string NOMBRE = (string)xlHoja1.get_Range("D" + j, Missing.Value).Text; if (NOMBRE == null || NOMBRE == "") NOMBRE = "SIN NOMBRE";
                        string PIEZA = (string)xlHoja1.get_Range("E" + j, Missing.Value).Text; if (PIEZA == null || PIEZA == "") PIEZA = "0";
                        string DESCRIPCION = (string)xlHoja1.get_Range("F" + j, Missing.Value).Text; if (DESCRIPCION == null || DESCRIPCION == "") DESCRIPCION = "SIN DESCRIPCIÓN";
                        string COLOR = (string)xlHoja1.get_Range("G" + j, Missing.Value).Text; if (COLOR == null || COLOR == "") COLOR = "SIN COLOR";
                        string CAPACIDAD = (string)xlHoja1.get_Range("H" + j, Missing.Value).Text; if (CAPACIDAD == null || CAPACIDAD == "") CAPACIDAD = "0";
                        string MATERIAL = (string)xlHoja1.get_Range("I" + j, Missing.Value).Text; if (MATERIAL == null || MATERIAL == "") MATERIAL = "";
                        string ACTIVO = (string)xlHoja1.get_Range("J" + j, Missing.Value).Text; if (ACTIVO == null || ACTIVO == "") ACTIVO = "Inactivo";
                        bool isActivo = false;
                        if (ACTIVO.Equals("Activo")) isActivo = true;
                        string MINIMO = (string)xlHoja1.get_Range("K" + j, Missing.Value).Text; if (MINIMO == null || MINIMO == "") MINIMO = "0";
                        string MAXIMO = (string)xlHoja1.get_Range("L" + j, Missing.Value).Text; if (MAXIMO == null || MAXIMO == "") MAXIMO = "0";
                        string EXISTENTE = (string)xlHoja1.get_Range("M" + j, Missing.Value).Text; if (EXISTENTE == null || EXISTENTE == "") EXISTENTE = "0";
                        string RESERVADO = (string)xlHoja1.get_Range("N" + j, Missing.Value).Text; if (RESERVADO == null || RESERVADO == "") RESERVADO = "0";
                        string FECHAARRIVO = (string)xlHoja1.get_Range("O" + j, Missing.Value).Text; if (FECHAARRIVO == null || FECHAARRIVO == "") FECHAARRIVO = DateTime.Now.ToString();
                        string PRECIO1 = (string)xlHoja1.get_Range("P" + j, Missing.Value).Text; if (PRECIO1 == null || PRECIO1 == "") PRECIO1 = "0";
                        string PRECIO2 = (string)xlHoja1.get_Range("Q" + j, Missing.Value).Text; if (PRECIO2 == null || PRECIO2 == "") PRECIO2 = "0";
                        string PRECIO3 = (string)xlHoja1.get_Range("R" + j, Missing.Value).Text; if (PRECIO3 == null || PRECIO3 == "") PRECIO3 = "0";
                        string MEDIDA = (string)xlHoja1.get_Range("S" + j, Missing.Value).Text; if (MEDIDA == null || MEDIDA == "") PRECIO3 = "0";
                        string ESPRINCIPAL = (string)xlHoja1.get_Range("T" + j, Missing.Value).Text; if (ESPRINCIPAL == null || ESPRINCIPAL == "") ESPRINCIPAL = "0";
                        bool isPrincipal = false;
                        if (ESPRINCIPAL.Equals("Sí")) isPrincipal = true;
                        string CODIGOPRINCIPAL = (string)xlHoja1.get_Range("U" + j, Missing.Value).Text; if (CODIGOPRINCIPAL == null || CODIGOPRINCIPAL == "") CODIGOPRINCIPAL = CODIGO;
                        j++;

                        string ProductoId = string.Empty;
                        var productoIdEncontrado = contexto.Producto.Where(m => m.Nombre == NOMBRE).FirstOrDefault();
                        var categoria = contexto.Categoria.Where(m => m.Descripcion == CATEGORIA).FirstOrDefault();

                        conecta.Abrirconexion();
                        conecta.comm = conecta.con.CreateCommand();
                        conecta.comm.CommandType = CommandType.StoredProcedure;

                        if (productoIdEncontrado == null)
                        {
                            conecta.comm.CommandText = "InsertaProducto";
                        }
                        else
                        {
                            conecta.comm.CommandText = "ActualizaProducto";
                            conecta.comm.Parameters.Add("@ProductoId", SqlDbType.NVarChar).Value = productoIdEncontrado.ProductoId;
                        }
                        conecta.comm.Parameters.Add("@CategoriaId", SqlDbType.Int).Value = categoria.CategoriaId;
                        conecta.comm.Parameters.Add("@Codigo", SqlDbType.NVarChar).Value = CODIGO;
                        conecta.comm.Parameters.Add("@CodigoBarra", SqlDbType.NVarChar).Value = CODIGOBARRA;
                        conecta.comm.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = NOMBRE;
                        conecta.comm.Parameters.Add("@Pieza", SqlDbType.Int).Value = PIEZA;
                        conecta.comm.Parameters.Add("@Descripcion", SqlDbType.NVarChar).Value = DESCRIPCION;
                        conecta.comm.Parameters.Add("@Color", SqlDbType.NVarChar).Value = COLOR;
                        conecta.comm.Parameters.Add("@Capacidad", SqlDbType.NVarChar).Value = CAPACIDAD;
                        conecta.comm.Parameters.Add("@Material", SqlDbType.NVarChar).Value = MATERIAL;
                        conecta.comm.Parameters.Add("@Activo", SqlDbType.Bit).Value = isActivo;
                        conecta.comm.Parameters.Add("@Medida", SqlDbType.NVarChar).Value = MEDIDA;
                        conecta.comm.Parameters.Add("@EsPrincipal", SqlDbType.Bit).Value = isPrincipal;
                        conecta.comm.Parameters.Add("@CodigoPrincipal", SqlDbType.NVarChar).Value = CODIGOPRINCIPAL;

                        if (productoIdEncontrado == null) conecta.comm.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        conecta.comm.ExecuteNonQuery();
                        if (productoIdEncontrado == null) ProductoId = conecta.comm.Parameters["@id"].Value.ToString();
                        conecta.CierraConexion();

                        //ProductoExistencia
                        conecta.Abrirconexion();
                        conecta.comm = conecta.con.CreateCommand();
                        conecta.comm.CommandType = CommandType.StoredProcedure;
                        if (productoIdEncontrado == null)
                        {
                            conecta.comm.CommandText = "InsertaProductoExistencia";
                            conecta.comm.Parameters.Add("@ProductoId", SqlDbType.Int).Value = ProductoId;
                        }
                        else
                        {
                            conecta.comm.CommandText = "ActualizaProductoExistencia";
                            conecta.comm.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoIdEncontrado.ProductoId;
                        }

                        conecta.comm.Parameters.Add("@Minimo", SqlDbType.Int).Value = int.Parse(MINIMO);
                        conecta.comm.Parameters.Add("@Maximo", SqlDbType.Int).Value = int.Parse(MAXIMO);
                        conecta.comm.Parameters.Add("@Existente", SqlDbType.Int).Value = int.Parse(EXISTENTE);
                        conecta.comm.Parameters.Add("@Reservado", SqlDbType.Int).Value = int.Parse(RESERVADO);
                        conecta.comm.Parameters.Add("@FechaArribo", SqlDbType.DateTime).Value = DateTime.Parse(FECHAARRIVO);
                        conecta.comm.ExecuteNonQuery();
                        conecta.CierraConexion();

                        //Precio
                        conecta.Abrirconexion();
                        conecta.comm = conecta.con.CreateCommand();
                        conecta.comm.CommandType = CommandType.StoredProcedure;
                        if (productoIdEncontrado == null)
                        {
                            conecta.comm.CommandText = "InsertaProductoPrecio";
                            conecta.comm.Parameters.Add("@ProductoId", SqlDbType.Int).Value = ProductoId;
                        }
                        else
                        {
                            conecta.comm.CommandText = "ActualizaProductoPrecio";
                            conecta.comm.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoIdEncontrado.ProductoId;
                        }
                        conecta.comm.Parameters.Add("@Precio1", SqlDbType.Float).Value = float.Parse(PRECIO1);
                        conecta.comm.Parameters.Add("@Precio2", SqlDbType.Float).Value = float.Parse(PRECIO2);
                        conecta.comm.Parameters.Add("@Precio3", SqlDbType.Float).Value = float.Parse(PRECIO3);
                        conecta.comm.ExecuteNonQuery();
                        conecta.CierraConexion();

                      
                    }
                    xlApp.Workbooks.Close();
                    xlApp.Application.Quit();
                    xlApp.Quit();
                    System.Diagnostics.Process[] myProcesses;
                    myProcesses = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                    foreach (System.Diagnostics.Process instance in myProcesses)
                    {
                        instance.CloseMainWindow();
                        instance.Kill();
                        instance.Close();
                    }
                    //---------------------
                    resultado.Exito = true;
                    resultado.MensajeExito = "Se cargo correctamente su información de productos.";

                }
            }
            catch (Exception ex)
            {
                
                resultado.Error = true;
                resultado.MensajeError = ex.Message;
            }
            return resultado;
        }




    }



}