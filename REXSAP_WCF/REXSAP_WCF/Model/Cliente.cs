using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace REXSAP_WCF.Model
{
    [DataContract]
    public class Cliente
    {
        private string codigo;
        private string denominacion;
        private string numRuc;
        private string direccion;
        private string codUbigeo;
        private string codVendedor;
        private string grupDescuento;
        private string estado;
        private string codFormaPago;
        private string codCanalCliente;
        private string tipoDoc;
        private string numDni;
        private string apellidoMat;
        private string apellidoPat;
        private string nombresCliente;
        private string nombreComercialNegocio;
        private string codUbigeoEnv;
        private string direccionEntregaPedido;
        private string ctipo_negocio;

        public Cliente()
        {
            codigo = "";
            denominacion = "";
            numRuc = "";
            direccion = "";
            codUbigeo = "";
            codVendedor = "";
            grupDescuento = "";
            estado = "";
            codFormaPago = "";
            codCanalCliente = "";
            tipoDoc = "";
            numDni = "";
            apellidoMat = "";
            apellidoPat = "";
            nombresCliente = "";
            nombreComercialNegocio = "";
            codUbigeoEnv = "";
            direccionEntregaPedido = "";
            ctipo_negocio = "";

        }

        [DataMember(Name = "KUNNR")]
        public string Codigo { get => codigo; set => codigo = value; }
        [DataMember(Name = "NAME1")]
        public string Denominacion { get => denominacion; set => denominacion = value; }
        [DataMember(Name = "STCD1")]
        public string NumRuc { get => numRuc; set => numRuc = value; }
        [DataMember(Name = "DIRECCION")]
        public string Direccion { get => direccion; set => direccion = value; }
        [DataMember(Name = "REGIOGROUP")]
        public string CodUbigeo { get => codUbigeo; set => codUbigeo = value; }
        [DataMember(Name = "KVGR1")]
        public string CodVendedor { get => codVendedor; set => codVendedor = value; }
        [DataMember(Name = "KONDA")]
        public string GrupDescuento { get => grupDescuento; set => grupDescuento = value; }
        [DataMember(Name = "ESTADO")]
        public string Estado { get => estado; set => estado = value; }
        [DataMember(Name = "ZTERM")]
        public string CodFormaPago { get => codFormaPago; set => codFormaPago = value; }
        [DataMember(Name = "VTWEG")]
        public string CodCanalCliente { get => codCanalCliente; set => codCanalCliente = value; }
        [DataMember(Name = "TIPODOC")]
        public string TipoDoc { get => tipoDoc; set => tipoDoc = value; }
        [DataMember(Name = "ZSTCD1")]
        public string NumDni { get => numDni; set => numDni = value; }
        [DataMember(Name = "ZNAME1")]
        public string ApellidoPat { get => apellidoPat; set => apellidoPat = value; }
        [DataMember(Name = "ZNAME2")]
        public string ApellidoMat { get => apellidoMat; set => apellidoMat = value; }
        [DataMember(Name = "ZNAME3")]
        public string NombresCliente { get => nombresCliente; set => nombresCliente = value; }
        [DataMember(Name = "NAMEC")]
        public string NombreComercialNegocio { get => nombreComercialNegocio; set => nombreComercialNegocio = value; }
        [DataMember(Name = "REGIOGROUP2")]
        public string CodUbigeoEnv { get => codUbigeoEnv; set => codUbigeoEnv = value; }
        [DataMember(Name = "STREET")]
        public string DireccionEntregaPedido { get => direccionEntregaPedido; set => direccionEntregaPedido = value; }

        [DataMember(Name = "KDGRP")]
        public string Ctipo_negocio { get => ctipo_negocio; set => ctipo_negocio = value; }
    }
}