﻿using RFC.Model;
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

        public BdConnection()
        {
            conn = new SqlConnection("Data Source=172.31.236.221;" +
               "Initial Catalog=rex;" +
               "User Id=rexdb;Password=rexdb2019;" +
               "connect timeout=2000;");
        }

        public void RecibirStock(DataTable dt)
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
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

        public List<TI_MATNR> GetMaterial()
        {
            List<TI_MATNR> mat = new List<TI_MATNR>();
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select right('00000000000000000'+carticulos_id,18) articulos from _articulos", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mat.Add(new TI_MATNR() {
                            SIGN ="I",
                            OPTION ="EQ",
                            LOW = reader[0].ToString(),

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
            return mat;

        }

    }
}
