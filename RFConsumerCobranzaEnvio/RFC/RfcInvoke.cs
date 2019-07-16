using RFC.Model;
using SAP.Middleware.Connector;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public RfcInvoke(string I_BUDAT)
        {
            this.I_BUDAT = I_BUDAT;
        }

        public RfcInvoke() {
        }
        public RfcInvoke(string FEC_INI, string FEC_FIN)
        {
            this.iM_FEC_INI = FEC_INI;
            this.iM_FEC_FIN = FEC_FIN;
        }

        public bool EnviarDatosCobranza(List<Cobranza> cobranzas)
        {
            RfcConfigParameters rfc = new RfcConfigParameters();
            rfc.Add(RfcConfigParameters.Name, "Desarrollo");
            rfc.Add(RfcConfigParameters.AppServerHost, "10.16.1.30");
            rfc.Add(RfcConfigParameters.Client, "600");
            rfc.Add(RfcConfigParameters.User, "REXSAP2");
            rfc.Add(RfcConfigParameters.Password, "Rexsap01");
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

            IRfcFunction function = rfcDest.Repository.CreateFunction("ZSD_REXSAP_008");// A la espera del nombre de la funcio
            IRfcTable doc = function.GetTable("M_PEDIDOS");//A la espera del nombre de la tabla
            //     doc.Insert();
            doc.Insert(cobranzas.Count());
           
            for (int i = 0; i <cobranzas.Count(); i++)
            {
                doc.CurrentIndex = i;

                doc.SetValue("KUNNR",cobranzas[i].Kunnr);
                doc.SetValue("BUKRS",cobranzas[i].Bukrs);
                doc.SetValue("BELNR",cobranzas[i].Belnr);
                doc.SetValue("GJAHR",cobranzas[i].Gjahr);
                doc.SetValue("BUDAT",cobranzas[i].Budat);
                doc.SetValue("BUZEI",cobranzas[i].Buzei);
                doc.SetValue("ZUONR",cobranzas[i].Zuonr);
                doc.SetValue("XBLNR",cobranzas[i].Xblnr);
                doc.SetValue("BLART",cobranzas[i].Blart);
                doc.SetValue("SHKZG",cobranzas[i].Shkzg);
                doc.SetValue("WAERS",cobranzas[i].Waers);
                doc.SetValue("DMBTR",cobranzas[i].Dmbtr);
                doc.SetValue("WRBTR",cobranzas[i].Wrbtr);
                doc.SetValue("ZFBDT",cobranzas[i].Zfbdt);
                doc.SetValue("DATS",cobranzas[i].Dats);
                doc.SetValue("UZEIT",cobranzas[i].Uzeit);
            }
            try {
                function.Invoke(rfcDest);
                return true;
            } catch (Exception e) {
                return false;
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
