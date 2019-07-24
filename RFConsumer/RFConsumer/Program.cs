using RFC;
using System;
using System.Collections.Generic;
using System.Data;
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
            string g = DateTime.Now.Date.ToString();
            g.Count();
            RfcInvoke rfcs = new RfcInvoke();
            BdConnection bd = new BdConnection();
            Console.WriteLine("Recuperando la información.Espere un momento...");
            DataTable dt = rfcs.ObtenerMovimientos();
           
            if (dt != null)
            {
                Console.WriteLine("Se recupero " + dt.Rows.Count.ToString() + " registros");
                Console.WriteLine("Se procesara la información en la base de datos.Espere un momento...");

                if (bd.AgregarMovimientosAlmacen(dt))
                {
                    Console.WriteLine("Se proceso la información con éxito");

                }
                else
                {
                    Console.WriteLine("Hubo un problema al procesar la información");

                }

            }
            else {

                Console.WriteLine("Hubo un problema al recuperar la información");
            }
        

            //   RfcInvoke rfcs = new RfcInvoke("20190626");
            //  RfcInvoke rfcs = new RfcInvoke("20190619");


        }

    }
}

