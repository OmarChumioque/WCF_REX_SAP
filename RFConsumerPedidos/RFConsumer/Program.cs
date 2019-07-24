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
          
            BdConnection bd = new BdConnection();
            RfcInvoke rfcs = new RfcInvoke();
            List<Pedido> pedidos = bd.ObtenerPedidos();
            List<DataTable> listTable = rfcs.IngresarPedidos(pedidos);
            bd.ResultadoPedidos(listTable[0]);
            bd.MensajesResultado(listTable[1]);
            //Console.ReadLine();

        }

    }
}

