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
          Console.WriteLine("Recuperando la información.Espere un momento...");
          DataTable tableCobranza= rfcs.ObtenerDatosCobranza();
            if (tableCobranza != null)
            {
                Console.WriteLine("Se recupero " + tableCobranza.Rows.Count.ToString() + "  registros");
                Console.WriteLine("Se guardara la informacion en la base de datos.Espere un momento...");
                if (bd.AgregarDatosCobranza(tableCobranza))
                {
                    Console.WriteLine("Se procesaron todos los registros con éxito");
                }
                else {

                    Console.WriteLine("Hubo un problema al registrar la información");
                }
            }
            else {
                Console.WriteLine("No se recupero informacion");
            }
          //  Console.Write(seEjecuto);
          Console.ReadLine();

        }

    }
}

