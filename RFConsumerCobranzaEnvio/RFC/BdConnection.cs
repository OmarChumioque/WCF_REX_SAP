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
        /*

        public List<Cobranza> ObtenerCobranzas() {

            List<Cobranza> list = new List<Cobranza>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_sap_requestCobranza", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    Cobranza c = new Cobranza();
                    c.Kunnr = reader["KUNNR"].ToString();
                    c.Bukrs = reader["BUKRS"].ToString();
                    c.Belnr = reader["BELNR_D"].ToString();
                    c.Gjahr = Convert.ToInt32(reader["GJAHR"]);
                    c.Buzei = Convert.ToInt32(reader["BUZEI"]);
                    c.Budat = Convert.ToDateTime( reader["BUDAT"]);
                    c.Zuonr =Convert.ToString( reader["ZUONR"]);
                    c.Xblnr = Convert.ToString(reader["XBLNR"]);
                    c.Blart = Convert.ToString(reader["BLART"]);
                    c.Shkzg = Convert.ToString(reader["SHKZG"]);
                    c.Waers = Convert.ToString(reader["WAERS"]);
                    c.Dmbtr = Convert.ToDecimal(reader["DMBTR"]);
                    c.Wrbtr= Convert.ToDecimal(reader["WRBTR"]);
                    c.Zfbdt=Convert.ToDateTime(reader["ZFBDT"]);
                    c.Dats = Convert.ToDateTime(reader["DATS"]);
                    c.Uzeit = Convert.ToString(reader["UZEIT"]);
                    list.Add(c);

                }

            }
            catch (Exception e)
            {
                list = null;

            }
            finally {
                conn.Close();
            }

                return list;

        }
        */

        public DataTable ObtenerCobranzas()
        {

            DataTable dt = new DataTable(); ;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_sap_requestCobranza", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception e)
            {
                dt = null;

            }
            finally
            {
                conn.Close();
            }

            return dt;

        }
        public List<Pedido> ObtenerPedidos() {

            List<Pedido> list = new List<Pedido>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("pintpedido", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pedido p = new Pedido();
                    p.Bstkd = reader["BSTKD"].ToString();
                    p.Kunnr = reader["KUNNR"].ToString();
                    p.Audat =Convert.ToDateTime(reader["AUDAT"]);
                    p.Zterm = reader["ZTERM"].ToString();
                    p.Auart = reader["AUART"].ToString();
                    p.Waerk = reader["WAERK"].ToString();
                    p.Netwr = Convert.ToDecimal(reader["NETWR"].ToString());
                    p.Matnr = reader["MATNR"].ToString();
                    p.Kwmeng = Convert.ToDecimal(reader["KWMENG"].ToString());
                    p.Netpr = Convert.ToDecimal(reader["NETPR"].ToString());
                    p.Pstyv = reader["PSTYV"].ToString();
                    p.Vbeln = reader["VBELN"].ToString();
                    list.Add(p);
                }


            }
            catch (Exception e)
            {
                e.ToString();
                list = new List<Pedido>();
            }
            finally {

                conn.Close();

            }

            return list;
        }

        public void AgregarMovimientosAlmacen(DataTable dt)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete DocSapStock", conn);
                cmd.ExecuteNonQuery();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn);
                    bulk.DestinationTableName = "DocSapStock";
                    bulk.ColumnMappings.Add("WERKS", "WERKS");
                    bulk.ColumnMappings.Add("LGORT", "LGORT");
                    bulk.ColumnMappings.Add("MATNR", "MATNR");
                    bulk.ColumnMappings.Add("VKUML", "VKUML");
                    bulk.ColumnMappings.Add("MEINS", "MEINS");
                    bulk.ColumnMappings.Add("ZDATE", "ZDATE");
                    bulk.WriteToServer(dt);
                    SqlCommand stored = new SqlCommand("pIntmovalmacenstock", conn);
                    stored.CommandType = CommandType.StoredProcedure;
                    stored.ExecuteNonQuery();
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
