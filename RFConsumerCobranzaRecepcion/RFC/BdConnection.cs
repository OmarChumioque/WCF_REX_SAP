using RFC.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFC
{
    public class BdConnection
    {
        SqlConnection conn;

        public BdConnection() {

            conn = new SqlConnection("Data Source=172.31.236.221;" +
               "Initial Catalog=rex;" +
               "User Id=rexdb;Password=rexdb2019;" +
               "connect timeout=2000;");
        }

       
        public void AgregarDatosCobranza(DataTable dt)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete DocSAPCobranzaRec", conn);
                cmd.ExecuteNonQuery();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn);
                    bulk.DestinationTableName = "DocSAPCobranzaRec";
                    bulk.ColumnMappings.Add("KUNNR", "KUNNR");
                    bulk.ColumnMappings.Add("BURKS", "BURKS");
                    bulk.ColumnMappings.Add("BELNR_D", "BELNR_D");
                    bulk.ColumnMappings.Add("GJAHR", "GJAHR");
                    bulk.ColumnMappings.Add("BUZEI", "BUZEI");
                    bulk.ColumnMappings.Add("BUDAT", "BUDAT");
                    bulk.ColumnMappings.Add("DZUONR", "DZUONR");
                    bulk.ColumnMappings.Add("XBLNR1", "XBLNR1");
                    bulk.ColumnMappings.Add("SHKZG", "SHKZG");
                    bulk.ColumnMappings.Add("WAERS", "WAERS");
                    bulk.ColumnMappings.Add("DMBTR", "DMBTR");
                    bulk.ColumnMappings.Add("WRBTR", "WRBTR");
                    bulk.ColumnMappings.Add("DZFBDT", "DZFBDT");
                    bulk.ColumnMappings.Add("DATS", "DATS");
                    bulk.ColumnMappings.Add("UZEIT", "UZEIT");

                    bulk.WriteToServer(dt);
                   /* SqlCommand stored = new SqlCommand("pIntmovalmacenstock", conn);
                    stored.CommandType = CommandType.StoredProcedure;
                    stored.ExecuteNonQuery();*/
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally {
                if (conn.State == ConnectionState.Open) {
                    conn.Close();
                }
            }




        } 

    }
}
