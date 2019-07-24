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
           RfcInvoke rfcs = new RfcInvoke();
           Console.WriteLine("Recuperando los registros...");
           DataTable dt= rfcs.ObtenerStock();
           Console.WriteLine("Se obtuvieron ->" + dt.Rows.Count.ToString() + " registros.");
           Console.WriteLine("Se registraran los datos en la base de datos");
           bool ret=bd.RecibirStock(dt);
            if (ret)
            {
                Console.WriteLine("Se procesaron los registros");
            }
            else {
                Console.WriteLine("Hubo un incoveniente al guardar la informacion en la base de datos");
            }
        }
    }
}

