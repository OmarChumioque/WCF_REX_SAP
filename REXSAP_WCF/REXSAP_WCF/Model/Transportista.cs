using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace REXSAP_WCF.Model
{
    [DataContract]
    public class Transportista
    {
        private string idProveedor;
        private string nombre;

        [DataMember(Name = "LIFNR")]
        public string IdProveedor { get => idProveedor; set => idProveedor = value; }
        [DataMember(Name = "NAME1")]
        public string Nombre { get => nombre; set => nombre = value; }
    }
}