using RFC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           RfcInvoke rfcs = new RfcInvoke("20190619");
            //   RfcInvoke rfcs = new RfcInvoke("20190626");
            //  RfcInvoke rfcs = new RfcInvoke("20190619");
            rfcs.WriteToFile();
            Console.Write("Se ejecuto");
            Console.ReadLine();
           


        }

    }
}

