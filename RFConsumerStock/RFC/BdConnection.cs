﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFC
{
    class BdConnection
    {
        SqlConnection conn;

        public BdConnection() {

            conn = new SqlConnection("Data Source=185.144.157.97;" +
                "Initial Catalog=Rex;" +
                "User Id=omarch1409;Password=1409Chumioque;" +
                "connect timeout=2000;");

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
