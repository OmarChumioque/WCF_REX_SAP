using RFC.Model;
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


        public RfcInvoke() {
        }

        public bool EnviarDatosCobranza(DataTable dt)
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

            IRfcFunction function = rfcDest.Repository.CreateFunction("ZSD_REXSAP_010");// A la espera del nombre de la funcio
            IRfcTable doc = function.GetTable("TI_DOCREXSAP");//A la espera del nombre de la tabla
            RfcStructureMetadata  metadata=  doc.Metadata.LineType;
         
            List<string> nombresColumna = new List<string>();
         

            for (int i = 0; i < metadata.FieldCount; i++) {
                nombresColumna.Add(doc.GetElementMetadata(i).Name);
            }
            foreach (DataRow row in dt.Rows) {

                doc.Append();

                for (int i = 0; i < dt.Columns.Count; i++) {

                    if (ExisteNombreColumna(nombresColumna, dt.Columns[i].ColumnName)) {
                        doc.SetValue(dt.Columns[i].ColumnName, row[dt.Columns[i].ColumnName]);

                    }

                }
                
            }

         
            try {
                function.Invoke(rfcDest);
                return true;
            } catch (Exception e) {
                return false;
            }
          
        }

        private bool ExisteNombreColumna(List<string> columnas,string nombreBuscar) {

            for (int i = 0; i < columnas.Count; i++) {
                if (columnas[i].Equals(nombreBuscar)) {
                   return true;
                 }
            }
            return false;
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
