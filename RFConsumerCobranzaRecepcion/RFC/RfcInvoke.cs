using RFC.Model;
using SAP.Middleware.Connector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace RFC
{
    public class RfcInvoke
    {


        public RfcInvoke() {
        }

        public DataTable ObtenerDatosCobranza()
        {

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
         

            try
            {
                rfcDest = RfcDestinationManager.GetDestination(rfc);
            }
            catch (Exception e)
            {
                e.ToString();
            }

           try
            {
                IRfcFunction function = rfcDest.Repository.CreateFunction("ZSD_REXSAP_009");
                RfcRepository rfcRep = null;

                IRfcTable cliente;
                cliente = function.GetTable("TI_KUNNR");

                BdConnection bd = new BdConnection();

                foreach (Cliente cli in bd.GetCliente())
                {
                    cliente.Append();
                    cliente.SetValue("SIGN", cli.SIGN);
                    cliente.SetValue("OPTION", cli.OPTION);
                    cliente.SetValue("LOW", cli.LOW);
                    cliente.SetValue("HIGH", "");
                }


                function.Invoke(rfcDest);

                IRfcTable doc = function.GetTable("IT_DOCSAPREX");
                DataTable table = IRfcTable_To_DataTable(doc, "IT_DOCSAPREX");
                return table;
            }
            catch (RfcBaseException e)
            {
                e.ToString();
                return null;    
            }

        }

        private  DataTable IRfcTable_To_DataTable(IRfcTable doc, string tableName) {
            DataTable table = new DataTable(tableName);

                for (int i = 0; i < doc.ElementCount; i++)
                {
                    RfcElementMetadata metadata = doc.GetElementMetadata(i);
                    if (metadata.DataType.ToString().Equals("CHAR"))
                    {
                        table.Columns.Add(metadata.Name, System.Type.GetType("System.String"));
                    }
                    else if (metadata.DataType.ToString().Equals("BCD"))
                    {
                        table.Columns.Add(metadata.Name, System.Type.GetType("System.Decimal"));
                    }
                    else if (metadata.DataType.ToString().Equals("DATE"))
                    {
                        table.Columns.Add(metadata.Name, System.Type.GetType("System.String"));
                    }
                    else if (metadata.DataType.ToString().Equals("TIME"))
                    {
                        table.Columns.Add(metadata.Name, System.Type.GetType("System.String"));

                    }
                    else if (metadata.DataType.ToString().Equals("NUM"))
                    {
                        table.Columns.Add(metadata.Name, System.Type.GetType("System.Int32"));

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
                    else if (metadata.DataType.ToString().Equals("BCD"))
                    {
                        dr[metadata.Name] = row.GetDecimal(metadata.Name);
                    }
                    else if (metadata.DataType.ToString().Equals("DATE"))
                    {
                        dr[metadata.Name] = row.GetString(metadata.Name);
                    }
                    else if (metadata.DataType.ToString().Equals("TIME"))
                    {
                        dr[metadata.Name] = row.GetString(metadata.Name);

                    }
                    else if (metadata.DataType.ToString().Equals("NUM"))
                    {
                        dr[metadata.Name] = row.GetInt(metadata.Name);

                    }
                }
                table.Rows.Add(dr);
            }
            return table;
        }
    }


}
