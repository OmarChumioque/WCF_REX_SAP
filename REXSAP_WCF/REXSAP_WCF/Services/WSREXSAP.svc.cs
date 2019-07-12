using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using REXSAP_WCF.BdConnect;
using REXSAP_WCF.Model;

namespace REXSAP_WCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WSREXSAP" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WSREXSAP.svc o WSREXSAP.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WSREXSAP : IWSREXSAP
    {
        BdConnection bd;
        public WSREXSAP()
        {
            bd = new BdConnection();
        }

        //Recepcion de lista de clientes
        public string IngresarClientes(List<Cliente> clientes)
        {
            if(clientes!=null)
                if(clientes.Count>0)
                     bd.RegistrarClientes(clientes);
            return "";
        }

        //Recepcion de lista de materiales
        public string IngresarMateriales(List<Material> materiales)
        {
            if (materiales != null) 
                if (materiales.Count > 0) 
                    bd.RegistrarMateriales(materiales);


            return "Se ingreso " + materiales.Count().ToString() + " materiales";
        }


        //Recepcion de lista de Transportistas
        public string IngresarTransportistas(List<Transportista> transportista)
        {

            if (transportista != null)
                if (transportista.Count > 0)
                    bd.RegistrarTransportistas(transportista);

            return "Se ingreso " + transportista.Count().ToString() + " transportistas";
        }


        //Recepcion de lista de Vendedores
        public string IngresarVendedores(List<Vendedor> vendedores)
        {



            if (vendedores != null)
                if (vendedores.Count > 0)
                    bd.RegistrarVendedores(vendedores);

            return "Se ingreso  " + vendedores.Count().ToString() + " vendedores";
        }
    }
}
