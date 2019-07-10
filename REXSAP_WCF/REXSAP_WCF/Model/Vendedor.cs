using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace REXSAP_WCF.Model
{
    [DataContract]
    public class Vendedor
    {

        private string idVendedor;
        private string nombre;
        [DataMember(Name = "VGR1")]
        public string IdVendedor { get => idVendedor; set => idVendedor = value; }
        [DataMember(Name = "BEZEI")]
        public string Nombre { get => nombre; set => nombre = value; }
    }
}