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
            /*
               conn = new SqlConnection("Data Source=185.144.157.97;" +
            "Initial Catalog=rex;" +
            "User Id=omarch1409;Password=1409Chumioque;" +
            "connect timeout=2000;"); */


        }

        public List<Pedido> ObtenerPedidos() {

            List<Pedido> list = new List<Pedido>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_sap_inpedido", conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@fechaPedido",dt));
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
                    p.Posnr = reader["POSNR"].ToString();
                    p.Ketdat =Convert.ToDateTime( reader["KETDAT"]);
                    p.Vrkme = reader["VRKME"].ToString();
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
        
        
        
        public void ResultadoPedidos(DataTable table) {
            try
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand("delete DocSapPedidos", conn);
                cmd.ExecuteNonQuery();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn);
                    bulk.DestinationTableName = "DocSapPedidos";
                    bulk.ColumnMappings.Add("BSTKD", "BSTKD");
                    bulk.ColumnMappings.Add("VBELN", "VBELN");
                    bulk.WriteToServer(table);
                   /* SqlCommand stored = new SqlCommand("pintPedidoResult", conn);
                    stored.CommandType = CommandType.StoredProcedure;
                    stored.ExecuteNonQuery();*/
                }

            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally {

                conn.Close();
            }

        }


        public void MensajesResultado(DataTable table) {

            try
            {

                conn.Open();
                /*
                SqlCommand cmd = new SqlCommand("delete DocSapPedidos", conn);
                cmd.ExecuteNonQuery();*/
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SqlBulkCopy bulk = new SqlBulkCopy(conn);
                    bulk.DestinationTableName = "sap_cab_Error";
                    bulk.ColumnMappings.Add("TYPE", "TYPE");
                    bulk.ColumnMappings.Add("BSTKD", "BSTKD");
                    bulk.ColumnMappings.Add("KUNNR", "KUNNR");
                    bulk.ColumnMappings.Add("MESSAGE", "MESSAGE");
                    bulk.WriteToServer(table);
                    /* SqlCommand stored = new SqlCommand("pintPedidoResult", conn);
                     stored.CommandType = CommandType.StoredProcedure;
                     stored.ExecuteNonQuery();*/
                }

            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally
            {

                conn.Close();
            }
        }

    }
}
