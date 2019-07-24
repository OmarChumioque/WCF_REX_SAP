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
               "Initial Catalog=rex;"+"User Id=rexdb;Password=rexdb2019;" +
               "connect timeout=2000;");

        }

        public bool AgregarMovimientosAlmacen(DataTable dt)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete DocSap", conn);
                cmd.ExecuteNonQuery();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn);
                    bulk.DestinationTableName = "DocSap";
                    bulk.ColumnMappings.Add("DOCEJEM", "DOCEJEM");
                    bulk.ColumnMappings.Add("BWART", "BWART");
                    bulk.ColumnMappings.Add("WERKS", "WERKS");
                    bulk.ColumnMappings.Add("LGORT", "LGORT");
                    bulk.ColumnMappings.Add("MATNR", "MATNR");
                    bulk.ColumnMappings.Add("VKUML", "LABST");
                    bulk.ColumnMappings.Add("MEINS", "MEINS");
                    bulk.ColumnMappings.Add("ZDATE", "DATE");
                    bulk.ColumnMappings.Add("TIP", "TIP");
                    bulk.WriteToServer(dt);
                    SqlCommand stored = new SqlCommand("Pintmovalmacen",conn);
                    stored.CommandType = CommandType.StoredProcedure;
                    stored.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
            finally {

                if (conn.State == ConnectionState.Open) {
                    conn.Close();
                }

                    
            }




        } 

    }
}
