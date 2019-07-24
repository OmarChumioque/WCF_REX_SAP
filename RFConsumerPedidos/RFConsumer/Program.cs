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
            List<DataTable> seEjecuto;
            BdConnection bd = new BdConnection();
            RfcInvoke rfcs = new RfcInvoke();
            List<Pedido> pedidos = bd.ObtenerPedidos();

            seEjecuto = rfcs.IngresarPedidos(pedidos);

            Console.Write(seEjecuto);
            //Console.ReadLine();

        }

    }
}

