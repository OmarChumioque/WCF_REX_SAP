﻿using RFC.Model;
using SAP.Middleware.Connector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFC
{
    public class RfcInvoke
    {

        private string iM_FEC_INI;
        private string iM_FEC_FIN;
        private string I_BUDAT;

        public RfcInvoke()
        {
    
        }

        public RfcInvoke(string I_BUDAT)
        {
            this.I_BUDAT = I_BUDAT;
        }


        public RfcInvoke(string FEC_INI, string FEC_FIN)
        {
            this.iM_FEC_INI = FEC_INI;
            this.iM_FEC_FIN = FEC_FIN;
        }

        public DataTable ObtenerMovimientos()
        {

            bool guardar = true;
            RfcConfigParameters rfc = new RfcConfigParameters();
            rfc.Add(RfcConfigParameters.Name, ConfigurationManager.AppSettings["Name"]);
            rfc.Add(RfcConfigParameters.AppServerHost, ConfigurationManager.AppSettings["AppServerHost"]);
            rfc.Add(RfcConfigParameters.Client, ConfigurationManager.AppSettings["Client"]);
            rfc.Add(RfcConfigParameters.User, ConfigurationManager.AppSettings["User"]);
            rfc.Add(RfcConfigParameters.Password, ConfigurationManager.AppSettings["Password"]);
            rfc.Add(RfcConfigParameters.SystemNumber, "00");
            rfc.Add(RfcConfigParameters.Language, "ES");
            rfc.Add(RfcConfigParameters.PoolSize, "5");
            rfc.Add(RfcConfigParameters.MaxPoolSize, "100");
            rfc.Add(RfcConfigParameters.IdleTimeout, "900");


            RfcDestination rfcDest = null;
            RfcRepository rfcRep = null;
            try
            {
                rfcDest = RfcDestinationManager.GetDestination(rfc);
                rfcRep = rfcDest.Repository;
            }     catch (Exception e) {
                e.ToString();
            }
          //  RfcRepository rfcRep = rfcDest.Repository;
            //  IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_007");
            //
            //
            //   IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_003");
            IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_007");

            function.SetValue("I_BUDAT", DateTime.Now.ToString("yyyy-MM-dd"));
            try
            {
                function.Invoke(rfcDest);
            }
            catch (RfcBaseException e)
            {
                guardar = false;
                Console.Write(e.ToString());
            }

            if (guardar)
            {

                //           IRfcTable doc = function.GetTable("MSTOCKS");
                IRfcTable doc = function.GetTable("MOVALMACEN");
                DataTable table = IRfcTable_To_DataTable(doc, "MOVALMACEN");
                return table;
           
            }
            else {
                return null;
            }
     
        }

        public DataTable IRfcTable_To_DataTable(IRfcTable doc, string tableName) {
            DataTable table = new DataTable(tableName);

            for (int i = 0; i < doc.ElementCount; i++)
            {
                RfcElementMetadata metadata = doc.GetElementMetadata(i);
                if (metadata.DataType.ToString().Equals("CHAR"))
                {
                    table.Columns.Add(metadata.Name, System.Type.GetType("System.String"));
                }
                if (metadata.DataType.ToString().Equals("BCD"))
                {
                    table.Columns.Add(metadata.Name, System.Type.GetType("System.Decimal"));
                }
                if (metadata.DataType.ToString().Equals("DATE"))
                {
                    table.Columns.Add(metadata.Name, System.Type.GetType("System.String"));
                }
            }

            foreach (IRfcStructure row in doc)
            {
                DataRow dr = table.NewRow();
                for (int i = 0; i < doc.ElementCount; i++)
                {
                    RfcElementMetadata metadata = doc.GetElementMetadata(i);
                    if (metadata.DataType.ToString().Equals("CHAR"))
                    {
                      
                            dr[metadata.Name] = row.GetString(metadata.Name);
               
                     
                    }
                    if (metadata.DataType.ToString().Equals("BCD"))
                    {
                        dr[metadata.Name] = row.GetDecimal(metadata.Name);
                    }
                    if (metadata.DataType.ToString().Equals("DATE"))
                    {
                        dr[metadata.Name] = row.GetString(metadata.Name);
                    }
                }
                table.Rows.Add(dr);
            }
            return table;
        }
    }


}
