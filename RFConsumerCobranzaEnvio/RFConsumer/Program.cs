﻿using RFC;
using RFC.Model;
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
           bool seEjecuto=false;
           BdConnection bd = new BdConnection();
           RfcInvoke rfcs = new RfcInvoke();
           List<Cobranza> list = bd.ObtenerCobranzas();
            if (list != null) {
                rfcs.
            }
        //   List<Pedido> list = bd.ObtenerPedidos();
      //     seEjecuto= rfcs.IngresarPedidos(list);
           Console.Write(seEjecuto);
           Console.ReadLine();

        }

    }
}
