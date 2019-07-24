using RFC.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFC
{
    public class RfcProcesos
    {
        BdConnection bd;
        RfcInvoke rfc;
        public RfcProcesos()
        {
            bd = new BdConnection();
            rfc = new RfcInvoke();
        }

        public string ProcesarMovimientosAlmacen(DateTime date)
        {

            DataTable dt = rfc.ObtenerMovimientosAlmacen(date);
            bool r = false;
            if (dt != null)
            {
                r = bd.AgregarMovimientosAlmacen(dt);
                if (r)
                {
                    return "Se proceso los datos con éxito";
                }
                else
                {
                    return "Hubo un problema al procesar los movimientos de álmacen";
                }

            }
            else
            {
                return "Existe un problema al recibir los movimientos de almacén";
            }


        }

        public string ProcesarDatosStock()
        {

            DataTable dt = rfc.ObtenerStock();
            string respuesta = "";
            bool r = false;
            if (dt != null)
            {

                r = bd.RecibirStock(dt);
                if (r)
                {
                    respuesta = "Se proceso los datos de stock con éxito";
                }
                else
                {
                    respuesta = "Hubo un problema al procesar los datos de stock";
                }
            }
            else
            {
                respuesta = "Hubo un problema al recibir los datos de stock";
            }


            return respuesta;
        }

        public string ProcesarPedidos(DateTime dateTime)
        {
            string respuesta = "";
            bool r = false;
            List<Pedido> list = bd.ObtenerPedidos(dateTime);
            if (list != null)
            {
                r = rfc.IngresarPedidos(list);
                if (r)
                {
                    respuesta = "Se enviaron los pedidos con éxito";
                }
                else
                {
                    respuesta = "Hubo un problema en el envio de pedidos.";
                }
            }
            else
            {

                respuesta = "No se obtuvieron los datos del pedido con éxito";
            }


            return respuesta;
        }

        public string ProcesarCobranzasEnvio()
        {
            string respuesta = "";
            List<Cobranza> cobranzas = bd.ObtenerCobranzas();
            if (cobranzas != null)
            {
                if (rfc.EnviarDatosCobranza(cobranzas))
                {
                    respuesta = "Se enviaron los datos de cobranza con exito";
                }
                else
                {
                    respuesta = "Hubo un inconveniente al enviar los datos de cobranza";
                }
            }
            else
            {
                respuesta = "Hubo un problema al obtener los datos de cobranza";
            }


            return respuesta;

        }

        public string ProcesarCobranzasRecepcion()
        {
            return "";
        }
    }

}
