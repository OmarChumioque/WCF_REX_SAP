using RFC.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
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

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connexion"].ConnectionString);    
                
        }

        public bool AgregarDatosCobranza(DataTable dt)
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
                    bulk.ColumnMappings.Add("BUKRS", "BUkRS");
                    bulk.ColumnMappings.Add("BELNR", "BELNR_D");
                    bulk.ColumnMappings.Add("GJAHR", "GJAHR");
                    bulk.ColumnMappings.Add("BUZEI", "BUZEI");
                    bulk.ColumnMappings.Add("BUDAT", "BUDAT");
                    bulk.ColumnMappings.Add("ZUONR", "DZUONR");
                    bulk.ColumnMappings.Add("XBLNR", "XBLNR1");
                    bulk.ColumnMappings.Add("SHKZG", "SHKZG");
                    bulk.ColumnMappings.Add("WAERS", "WAERS");
                    bulk.ColumnMappings.Add("DMBTR", "DMBTR");
                    bulk.ColumnMappings.Add("WRBTR", "WRBTR");
                    bulk.ColumnMappings.Add("ZFBDT", "DZFBDT");
                    bulk.ColumnMappings.Add("FECHA_ACCION", "DATS");
                    bulk.ColumnMappings.Add("HORA_ACCION", "UZEIT");
                    bulk.ColumnMappings.Add("BLART", "BLART");
                    
                    bulk.WriteToServer(dt);

                    SqlCommand stored = new SqlCommand("sp_sap_receiverCobranza", conn);
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

        public List<Cliente> GetCliente()
        {
            List<Cliente> material = new List<Cliente>();


            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select right('00000000'+ccod_clie, 10) cliente from _cliente", conn);
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        material.Add(new Cliente()
                        {
                            SIGN = "I",
                            OPTION = "EQ",
                            LOW = reader.GetString(0)
                        });
                    }
                }

            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return material;
        }

    }
}
