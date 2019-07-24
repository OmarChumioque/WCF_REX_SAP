using RFC.Model;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX_SAP_FORM.RFC
{
    public class RfcInvoke
    {

        private string iM_FEC_INI;
        private string iM_FEC_FIN;
        private string I_BUDAT;
        public RfcInvoke()
        {

        }



        public RfcInvoke(string FEC_INI, string FEC_FIN)
        {
            this.iM_FEC_INI = FEC_INI;
            this.iM_FEC_FIN = FEC_FIN;
        }

        private RfcConfigParameters GetConfigRFC()
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

            return rfc;
        }
        public DataTable ObtenerStock()
        {
            DataTable dt = new DataTable("");

            RfcDestination rfcDest = null;
            RfcRepository rfcRep = null;
            try
            {
                rfcDest = RfcDestinationManager.GetDestination(GetConfigRFC());
                rfcRep = rfcDest.Repository;
            }
            catch (Exception e)
            {
                e.ToString();
            }
            //  RfcRepository rfcRep = rfcDest.Repository;
            //  IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_007");
            //
            //
            //   IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_003");
            IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_006");

            //function.SetValue("I_BUDAT", I_BUDAT);
            try
            {
                function.Invoke(rfcDest);
                IRfcTable doc = function.GetTable("MSTOCKS");
                DataTable table = IRfcTable_To_DataTable(doc, "MSTOCKS");
                dt = table;

                return dt;
            }
            catch (RfcBaseException e)
            {
                e.ToString();
                return null;
            }



            //  BdConnection bd = new BdConnection();
            //   bd.RecibirStock(table);




        }

        public DataTable ObtenerMovimientosAlmacen(DateTime date)
        {
            bool guardar = true;
            RfcDestination rfcDest = null;
            RfcRepository rfcRep = null;

            try
            {
                rfcDest = RfcDestinationManager.GetDestination(GetConfigRFC());
                rfcRep = rfcDest.Repository;
            }
            catch (Exception e)
            {
                e.ToString();
            }
            IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_007");

            function.SetValue("I_BUDAT", date);
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
            else
            {
                return null;
            }
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

            IRfcFunction function = rfcDest.Repository.CreateFunction("ZSD_REXSAP_010");// A la espera del nombre de la funcio
            IRfcTable doc = function.GetTable("TI_DOCREXSAP");//A la espera del nombre de la tabla
            doc.Insert();
            doc.Insert(cobranzas.Count());

            for (int i = 0; i < cobranzas.Count(); i++)
            {
                doc.CurrentIndex = i;

                doc.SetValue("KUNNR", cobranzas[i].Kunnr);
                doc.SetValue("BUKRS", cobranzas[i].Bukrs);
                doc.SetValue("BELNR", cobranzas[i].Belnr);
                doc.SetValue("GJAHR", cobranzas[i].Gjahr);
                doc.SetValue("BUDAT", cobranzas[i].Budat);
                doc.SetValue("BUZEI", cobranzas[i].Buzei);
                doc.SetValue("ZUONR", cobranzas[i].Zuonr);
                doc.SetValue("XBLNR", cobranzas[i].Xblnr);
                doc.SetValue("BLART", cobranzas[i].Blart);
                doc.SetValue("SHKZG", cobranzas[i].Shkzg);
                doc.SetValue("WAERS", cobranzas[i].Waers);
                doc.SetValue("DMBTR", cobranzas[i].Dmbtr);
                doc.SetValue("WRBTR", cobranzas[i].Wrbtr);
                doc.SetValue("ZFBDT", cobranzas[i].Zfbdt);
                doc.SetValue("DATS", cobranzas[i].Dats);
                doc.SetValue("UZEIT", cobranzas[i].Uzeit);
            }
            try
            {
                function.Invoke(rfcDest);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool IngresarPedidos(List<Pedido> pedidos)
        {


            RfcDestination rfcDest = null;
            RfcRepository rfcRep = null;
            try
            {
                rfcDest = RfcDestinationManager.GetDestination(GetConfigRFC());

                //     doc.Insert();

            }
            catch (Exception e)
            {
                e.ToString();
            }

            IRfcFunction function = rfcDest.Repository.CreateFunction("ZSD_REXSAP_008");
            IRfcTable doc = function.GetTable("M_PEDIDOS");
            doc.Insert(pedidos.Count());

            for (int i = 0; i < pedidos.Count(); i++)
            {
                doc.CurrentIndex = i;
                doc.SetValue("BSTKD", pedidos[i].Bstkd);
                doc.SetValue("KUNNR", pedidos[i].Kunnr);
                doc.SetValue("AUDAT", pedidos[i].Audat);//DATE
                doc.SetValue("ZTERM", pedidos[i].Zterm);
                doc.SetValue("AUART", pedidos[i].Auart);
                doc.SetValue("WAERK", pedidos[i].Waerk);
                doc.SetValue("NETWR", Math.Round(pedidos[i].Netwr, 2));//DECIMAL
                doc.SetValue("MATNR", pedidos[i].Matnr);
                doc.SetValue("KWMENG", pedidos[i].Kwmeng);//DECIMAL
                doc.SetValue("KBETR", Math.Round(pedidos[i].Kbetr, 2));//DECIMAL
                doc.SetValue("PSTYV", pedidos[i].Pstyv);
                doc.SetValue("NETPR", Math.Round(pedidos[i].Netpr, 2));//DECIMAL
                doc.SetValue("VBELN", pedidos[i].Vbeln);
                doc.SetValue("KETDAT", pedidos[i].Ketdat);
                doc.SetValue("POSNR", pedidos[i].Posnr);
                doc.SetValue("VRKME", pedidos[i].Vrkme);

            }
            try
            {

                function.Invoke(rfcDest);
                IRfcTable doc2 = function.GetTable("TI_PEDIDOSAP");
                DataTable dt2 = IRfcTable_To_DataTable(doc2, "TI_PEDIDOSAP");
                dt2.Rows.Count.ToString();

                return true;
            }
            catch (Exception e)
            {
                e.ToString();

                return false;
            }

        }

        public DataTable ObtenerDatosCobranza()
        {

            RfcDestination rfcDest = null;

            try
            {
                rfcDest = RfcDestinationManager.GetDestination(GetConfigRFC());
            }
            catch (Exception e)
            {
                e.ToString();
            }

            try
            {
                IRfcFunction function = rfcDest.Repository.CreateFunction("ZSD_REXSAP_009");
                RfcRepository rfcRep = null;
                function.Invoke(rfcDest);
                IRfcTable doc = function.GetTable("IT_DOCSAPREX");
                DataTable table = IRfcTable_To_DataTable(doc, "IT_DOCSAPREX");
                return table;
            }
            catch (RfcBaseException e)
            {
                return null;
            }

        }

        private DataTable IRfcTable_To_DataTable(IRfcTable doc, string tableName)
        {
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
