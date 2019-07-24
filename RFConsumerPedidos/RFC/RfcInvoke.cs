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

        public List<DataTable> IngresarPedidos(List<Pedido> pedidos)
        {

            List<DataTable> list = new List<DataTable>();
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
            RfcRepository rfcRep = null;
            try
            {
                rfcDest = RfcDestinationManager.GetDestination(rfc);

                //     doc.Insert();

            }
            catch (Exception e)
            {
                e.ToString();
            }

            IRfcFunction function = rfcDest.Repository.CreateFunction("ZSD_REXSAP_008");
            IRfcTable doc = function.GetTable("M_PEDIDOS");
            doc.Insert(pedidos.Count());

            for (int i = 0; i <pedidos.Count(); i++)
            {
                doc.CurrentIndex = i;
                doc.SetValue("BSTKD",pedidos[i].Bstkd);
                doc.SetValue("KUNNR",pedidos[i].Kunnr);
                doc.SetValue("AUDAT",pedidos[i].Audat);//DATE
                doc.SetValue("ZTERM",pedidos[i].Zterm);
                doc.SetValue("AUART",pedidos[i].Auart);
                doc.SetValue("WAERK",pedidos[i].Waerk);
                doc.SetValue("NETWR",Math.Round( pedidos[i].Netwr,2));//DECIMAL
                doc.SetValue("MATNR",pedidos[i].Matnr);
                doc.SetValue("KWMENG",pedidos[i].Kwmeng);//DECIMAL
                doc.SetValue("KBETR",Math.Round(pedidos[i].Kbetr,2));//DECIMAL
                doc.SetValue("PSTYV",pedidos[i].Pstyv);
                doc.SetValue("NETPR",Math.Round(pedidos[i].Netpr, 2));//DECIMAL
                doc.SetValue("VBELN",pedidos[i].Vbeln);
                doc.SetValue("KETDAT",pedidos[i].Ketdat);
                doc.SetValue("POSNR",pedidos[i].Posnr);
                doc.SetValue("VRKME",pedidos[i].Vrkme);

            }
            try {
            
                function.Invoke(rfcDest);
                IRfcTable doc2 = function.GetTable("TI_PEDIDOSAP");
                IRfcTable doc3 = function.GetTable("TI_RETURN");
                DataTable dt2 = IRfcTable_To_DataTable(doc2, "TI_PEDIDOSAP");
                DataTable dt3= IRfcTable_To_DataTable(doc3, "TI_RETURN");
                list.Add(dt2);
                list.Add(dt3);
                
          //          return true;
            } catch (Exception e) {
                e.ToString();
            
          //      return false;
            }

            return list;
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
