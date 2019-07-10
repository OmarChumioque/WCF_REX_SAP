using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace REXSAP_WCF.Model
{

    [DataContract]
    public class Material
    {

        private string codMaterial;
        private string nombre;
        private string unidadConsumo;
        private decimal factorConversion;
        private string estado;
        private string unidadVenta;
        private decimal factorConversionVenta;

        [DataMember(Name = "MATNR")]
        public string CodMaterial { get => codMaterial; set => codMaterial = value; }
        [DataMember(Name = "MAKTX")]
        public string Nombre { get => nombre; set => nombre = value; }
        [DataMember(Name = "MEINS")]
        public string UnidadConsumo { get => unidadConsumo; set => unidadConsumo = value; }
        [DataMember(Name = "BASE")]
        public decimal FactorConversion { get => factorConversion; set => factorConversion = value; }
        [DataMember(Name = "MSTAE")]
        public string Estado { get => estado; set => estado = value; }
        [DataMember(Name = "FORMT")]
        public string UnidadVenta { get => unidadVenta; set => unidadVenta = value; }
        [DataMember(Name = "UMREX")]
        public decimal FactorConversionVenta { get => factorConversionVenta; set => factorConversionVenta = value; }
    }

}