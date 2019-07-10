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
            IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_007");
            //

            // IRfcFunction function = rfcRep.CreateFunction("ZSD_REXSAP_003");



            try
            {
                function.Invoke(rfcDest);
            }
            catch (RfcBaseException e)
            {
                Console.Write(e.ToString());
            }


          //  IRfcTable dest = function.GetTable("ET_RETURN");
            IRfcTable doc = function.GetTable("MVENDEDORES");
            for (int i = 0; i < doc.ElementCount; i++) {

                RfcElementMetadata metadata = doc.GetElementMetadata(i);
               
                Console.Write(metadata.Name.ToString());
            }
            

        }
    }


}
