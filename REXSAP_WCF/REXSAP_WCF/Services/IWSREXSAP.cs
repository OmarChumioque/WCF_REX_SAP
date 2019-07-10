using REXSAP_WCF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace REXSAP_WCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWSREXSAP" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWSREXSAP
    {
        [OperationContract]
        string IngresarClientes(List<Cliente> clientes);

        [OperationContract]
        string IngresarMateriales(List<Material> materiales);

        [OperationContract]
        string IngresarTransportistas(List<Transportista> transportista);
        [OperationContract]
        string IngresarVendedores(List<Vendedor> vendedores);

    }
}
