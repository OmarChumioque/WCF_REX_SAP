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


        public RfcInvoke(string FEC_INI, string FEC_FIN)
        {
            this.iM_FEC_INI = FEC_INI;
            this.iM_FEC_FIN = FEC_FIN;
        }

        public void WriteToFile()
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
            rfc.Add(RfcConfigParameters.IdleTimeout, "600");


            RfcDestination rfcDest = RfcDestinationManager.GetDestination(rfc);

            RfcRepository rfcRep = rfcDest.Repository;
            //  IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_007");
            //
            //


            //   IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_003");
            IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_007");

            function.SetValue("I_BUDAT", I_BUDAT);

            try
            {
                function.Invoke(rfcDest);
            }
            catch (RfcBaseException e)
            {
                Console.Write(e.ToString());
            }


            //  IRfcTable dest = function.GetTable("ET_RETURN");
            //  IRfcTable doc = function.GetTable("MVENDEDORES");
            IRfcTable doc = function.GetTable("MOVALMACEN");
            foreach (IRfcStructure row in doc) {
                for (int i = 0; i < doc.ElementCount; i++) {

                     RfcElementMetadata metadata = doc.GetElementMetadata(i);
               
                    Console.Write(metadata.Name+" -> "+row.GetString( metadata.Name)+"\n");
              }
            }

            Console.ReadLine();

        }
    }


}
