﻿using RFC;
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
           DataTable dt= bd.ObtenerCobranzas();
           if(dt!=null)
           rfcs.EnviarDatosCobranza(dt);
            
        //   List<Pedido> list = bd.ObtenerPedidos();
      //     seEjecuto= rfcs.IngresarPedidos(list);
           
        }

    }
}

