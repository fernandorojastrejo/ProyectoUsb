using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

/// <summary>
/// Summary description for conexionOle
/// </summary>
public class ConexionOle
{
    OleDbConnection miConn;
    //DataTable dtExcelRecords;
    string cadconexion;

    public ConexionOle()
    {
        cadconexion = ""; // Dts.Connections["Excel Connection Manager"].ConnectionString.ToString();
    }

    public OleDbConnection Abrir(string cadenacon)
    {
        try
        {
            //WebMsgBox.Show("Ruta oledb  "+cadenacon);
            miConn = new OleDbConnection(cadenacon);
            if (miConn.State == ConnectionState.Open)
            {
                miConn.Close();
            }
            miConn.Open();
            //WebMsgBox.Show("Conexion abierta");
            return miConn;
        }
        catch (Exception e) 
        {
            //WebMsgBox.Show(e.Message);
            Console.WriteLine(e.Message.ToString());
            return null;
        }

        
    }

    public void Cierra(OleDbConnection myConn)
    {
        myConn.Dispose();
        myConn.Close();
    }

    public DataTable Hoja(OleDbConnection myConn, string nom_hoja, string C1, string F1, string C2, string F2)
    {
        //OleDbCommand cmdExcel = new OleDbCommand();
        //OleDbDataAdapter oda = new OleDbDataAdapter();
        //cmdExcel.Connection = myConn;

        OleDbCommand oleDbCommand = new OleDbCommand();
        oleDbCommand.CommandType = System.Data.CommandType.Text;
        oleDbCommand.Connection = myConn;
        OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);

        DataTable dtExcelSheetName = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        //DataTable tablesInFile;
        //tablesInFile = myConn.GetSchema("Tables");
        //int tableCount = dtExcelSheetName.Rows.Count;
        int tableIndex = -1;
        int Indice = -1;
        //string currentTable = "";
        foreach (DataRow tableInFile in dtExcelSheetName.Rows)
        {
            tableIndex += 1;
            //currentTable = currentTable + "," + tableInFile["TABLE_NAME"].ToString();
            if (nom_hoja.ToUpper() == tableInFile["TABLE_NAME"].ToString().ToUpper())
            {
                Indice = tableIndex;
            }
        }



        string getExcelSheetName = dtExcelSheetName.Rows[Indice]["Table_Name"].ToString();

        if (C1 != "" && C2 != "")
        {
            oleDbCommand.CommandText = "SELECT * FROM [" + getExcelSheetName + C1 + F1 + ":" + C2 + F2 + "]";
        }
        else
            oleDbCommand.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";

        oleDbDataAdapter.SelectCommand = oleDbCommand;
        DataTable dtExcelRecords = new DataTable();
        try
        {
            oleDbDataAdapter.Fill(dtExcelRecords);
            return dtExcelRecords;
        }
        catch (Exception e)
        {
            return null;
        }
        
        
    }

    public string[] ObtieneRango(DataTable dtExcelRecords, int despliega, int iniciocolumna, int iniciofila)
    {
        int i;
        int j;
        string[] rango = new string[4];

        int k = dtExcelRecords.Columns.Count;
        int l = dtExcelRecords.Rows.Count;
        rango[0] = Convert.ToString(iniciocolumna);
        rango[1] = Convert.ToString(iniciofila + 4);
        rango[2] = Convert.ToString(k);
        rango[3] = Convert.ToString(l);
        string valor = "";
        for (i = iniciocolumna; i < k; i++)
        {
            //foreach (DataColumn column in dtExcelRecords.Columns)
            //{
            //if (column.ColumnName.Equals(column.ColumnName, StringComparison.OrdinalIgnoreCase)) return true;
            //Console.WriteLine("{0}, {1}, {2}", column, column.DataType, column.ColumnName);
            //}
            if (dtExcelRecords.Rows[0][i].ToString().Trim('\'') == "")
            {
                rango[2] = Convert.ToString(i - 1);
                break;
            }
        }
        rango[2] = Convert.ToString(i - 1);
        for (j = iniciofila; j < l; j++)
        {
            if (dtExcelRecords.Rows[j][1].ToString().Trim().Trim('\'') == "")
            {
                valor = "";
                for (i = iniciocolumna; i <= Convert.ToInt16(rango[2]); i++)
                {
                    valor = valor + dtExcelRecords.Rows[j][i].ToString().Trim('\'');
                }
                if (valor == "")
                {
                    rango[3] = Convert.ToString(j + 1);
                    break;
                }
            }
        }
        rango[3] = Convert.ToString(j + 1);

        return rango;
    }

    public string[] Hojas(OleDbConnection miConn)
    {
        DataTable tablesInFile;
        tablesInFile = miConn.GetSchema("Tables");
        int tableCount = tablesInFile.Rows.Count;
        string[] mishojas = new string[tableCount];
        //string currentTable = "";
        int k = 0;
        foreach (DataRow tableInFile in tablesInFile.Rows)
        {
            mishojas[k] = tableInFile["TABLE_NAME"].ToString();
            k++;
        }

        return mishojas;
    }

    //public string[] ObtieneColumnas(DataTable dtExcelRecords, int despliega, int iniciocolumna)
    //{
    //    string[] arreglo = new string[dtExcelRecords.Columns.Count];

    //    string currentTable = "";
    //    int i = 0;
    //    int j = 0;
    //    foreach (DataColumn column in dtExcelRecords.Columns)
    //    {
    //        if (i >= iniciocolumna)
    //        {
    //            arreglo[j] = column.ColumnName;
    //            if (j == 0) currentTable = currentTable + arreglo[j];
    //            else currentTable = currentTable + "," + arreglo[j];
    //            j++;
    //        }
    //        i++;
    //    }
    //    if (despliega == 1) //System.Windows.Forms.MessageBox.Show(currentTable);

    //    return arreglo;
    //}

    //public string[,] ObtieneDatos(DataTable dtExcelRecords, int despliega)
    //{
    //    int r = dtExcelRecords.Rows.Count;
    //    int c = dtExcelRecords.Columns.Count;
    //    string[,] arreglo = new string[c, r];

    //    string currentTable = "";
    //    for (int j = 0; j < dtExcelRecords.Rows.Count; j++)
    //    {
    //        for (int i = 0; i < dtExcelRecords.Columns.Count; i++)
    //        {
    //            //currentTable = currentTable + "," + dtExcelRecords.Rows[j][i].ToString();
    //            arreglo[i, j] = dtExcelRecords.Rows[j][i].ToString();
    //            if (i == 0) currentTable = currentTable + arreglo[i, j];
    //            else currentTable = currentTable + "," + arreglo[i, j];
    //        }
    //        currentTable = currentTable + Environment.NewLine;
    //    }
    //    if (despliega == 1) //System.Windows.Forms.MessageBox.Show(currentTable);

    //    return arreglo;
    //}

    //public string[] ObtieneDatosLista(DataTable dtExcelRecords, int despliega)
    //{
    //    int r = dtExcelRecords.Rows.Count;
    //    int c = dtExcelRecords.Columns.Count;
    //    int total = r * c;
    //    string[] arreglo = new string[total];

    //    string currentTable = "";
    //    int k = 0;
    //    for (int j = 0; j < dtExcelRecords.Rows.Count; j++)
    //    {
    //        for (int i = 0; i < dtExcelRecords.Columns.Count; i++)
    //        {
    //            arreglo[k] = dtExcelRecords.Rows[j][i].ToString();
    //            if (i == 0) currentTable = currentTable + arreglo[k];
    //            else currentTable = currentTable + "," + arreglo[k];
    //            k++;
    //        }
    //        currentTable = currentTable + Environment.NewLine;
    //    }
    //    if (despliega == 1) //System.Windows.Forms.MessageBox.Show(currentTable);

    //    return arreglo;
    //}


    public void AsignaNombres(DataTable dtExcelRecords, string[] campos, int bnd_nombre, int despliega)
    {
        if (bnd_nombre >= 0)
        {
            int i = 0;
            foreach (DataColumn column in dtExcelRecords.Columns)
            {
                if (bnd_nombre == 0) column.ColumnName = campos[i];
                else column.ColumnName = "F" + (i + 1);
                i++;
            }
        }

        if (despliega == 1)
        {
            string currentTable = "";
            int j = 0;
            foreach (DataColumn column in dtExcelRecords.Columns)
            {
                if (j == 0) currentTable = currentTable + column.ColumnName;
                else currentTable = currentTable + "," + column.ColumnName;
                j++;
            }
            //System.Windows.Forms.MessageBox.Show(currentTable);
        }

    }

    public DataRow[] Seleccion(DataTable dtExcelRecords, string expresion, int ncolumnas, int despliega)
    {
        string currentTable = "";
        DataRow[] result = dtExcelRecords.Select(expresion);
        if (ncolumnas == -1) ncolumnas = dtExcelRecords.Columns.Count;
        if (despliega == 1)
        {
            foreach (DataRow row in result)
            {
                for (int i = 0; i < ncolumnas; i++)
                {
                    if (i == 0) currentTable = row[i].ToString();
                    else currentTable = currentTable + "," + row[i].ToString();
                }
                //System.Windows.Forms.MessageBox.Show(currentTable);
            }
        }

        return result;
    }
}