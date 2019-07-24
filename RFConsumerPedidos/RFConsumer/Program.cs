using RFC;
using RFC.Model;
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
           bool seEjecuto=false;
           BdConnection bd = new BdConnection();
           RfcInvoke rfcs = new RfcInvoke();
        
         
           List<Pedido> list = bd.ObtenerPedidos(new DateTime(2019,7,23));
           Console.WriteLine("Se recuperaron ->"+list.Count().ToString() +" registros. " );

           List<DataTable> dataTables= rfcs.IngresarPedidos(list);
            bd.ResultadoPedidos(dataTables[0]);
            if (seEjecuto)
            {
        //        Console.WriteLine("Se envió los datos con éxito. ");
            }
            else {
         //       Console.WriteLine("Hubo un problema en el destino.");

            }
        }

    }
}

