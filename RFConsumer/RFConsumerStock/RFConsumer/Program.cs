using RFC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace RFConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            RfcInvoke rfcs = new RfcInvoke("01.01.2016", "01.07.2019");
            rfcs.WriteToFile();
            */
           BdConnection bd = new BdConnection();
           RfcInvoke rfcs = new RfcInvoke("20190619");
         
           DataTable dt= rfcs.ObtenerStock();
           bd.RecibirStock(dt);
        }

    }
}

