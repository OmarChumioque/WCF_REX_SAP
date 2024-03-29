﻿using REXSAP_WCF.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace REXSAP_WCF.BdConnect
{
    public class BdConnection
    {
        SqlConnection conn;

        public BdConnection()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connexion"].ConnectionString);

        }

        public void RegistrarClientes(List<Cliente> clientes) {
            SqlCommand cmd = null;
            try
            {
                conn.Open();
                for (int i = 0; i < clientes.Count; i++)
                {
                    Cliente it = clientes[i];
                    cmd = new SqlCommand("sp_sap_INCliente", conn);// Procedimiento ingresa o actualiza cliente recibido
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (it.Codigo != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@ccod_clie", it.Codigo));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@ccod_clie", ""));
                    }

                    if (it.Denominacion != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cnomb_clie", it.Denominacion));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cnomb_clie", ""));
                    }
                    
                    if (it.NumRuc != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cruc_clie", it.NumRuc));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cruc_clie",""));
                    }
                    if (it.Direccion != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@mdirec_clie", it.Direccion));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@mdirec_clie",""));
                    }

                    if (it.CodUbigeo != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cubigeo_clie", it.CodUbigeo));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cubigeo_clie", "999999"));
                    }
                    if (it.CodVendedor != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cid_vend", it.CodVendedor));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cid_vend", ""));
                    }

                    if (it.GrupDescuento != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cid_grupclie", it.GrupDescuento));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cid_grupclie",""));
                    }
                    if (it.Estado == "X")
                    {
                        cmd.Parameters.Add(new SqlParameter("@cid_stat_clie", "2"));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cid_stat_clie", "1"));
                    }

                    if (it.CodFormaPago != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cforma_pago_id", it.CodFormaPago));

                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cforma_pago_id",""));

                    }

                    if (it.CodCanalCliente != null) {
                        cmd.Parameters.Add(new SqlParameter("@cgiro_id", it.CodCanalCliente));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cgiro_id", ""));
                    }


                    if (it.TipoDoc != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@ctipo_documento_habitual", it.TipoDoc));

                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@ctipo_documento_habitual",""));

                    }

                    if (it.NumDni != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cdocumento_dni", it.NumDni));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cdocumento_dni", ""));
                    }
                   
                    if (it.ApellidoPat != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@a_paterno", it.ApellidoPat));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@a_paterno", ""));
                    }
                    if (it.ApellidoMat != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@a_materno", it.ApellidoMat));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@a_materno",""));
                    }

                    if (it.NombresCliente != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombres", it.NombresCliente));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@nombres", ""));
                    }

                    if (it.NombreComercialNegocio != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cnombre_comercial", it.NombreComercialNegocio));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cnombre_comercial", ""));
                    }

                    if (it.CodUbigeoEnv != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cubigeo_env", it.CodUbigeoEnv));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cubigeo_env", ""));
                    }

                    if (it.DireccionEntregaPedido != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@mdire_env", it.DireccionEntregaPedido));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@mdire_env", ""));
                    }

                    if (it.Ctipo_negocio != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@ctipo_negocio", it.Ctipo_negocio));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@ctipo_negocio", ""));
                    }

                    int a = cmd.ExecuteNonQuery();
                    a.ToString();
                }

            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally {
                if (conn.State == System.Data.ConnectionState.Open) {
                    conn.Close();
                }
            }
        }

        public void RegistrarTransportistas(List<Transportista> transportistas) {
            SqlCommand cmd = null;
            try
            {
                conn.Open();
                for (int i = 0; i < transportistas.Count; i++) {
                    Transportista it = transportistas[i];
                    cmd = new SqlCommand("pWStransportista", conn);// Procedimiento ingresa o actualiza transportista recibido
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    if (it.IdProveedor != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cproveedor_id", it.IdProveedor));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cproveedor_id", ""));
                    }

                    if (it.Nombre != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cproveedor_nombre", it.Nombre));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cproveedor_nombre", ""));
                    }
                   
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally {
                if (conn.State == System.Data.ConnectionState.Open) {
                    conn.Close();
                }
            }


        }

        public void RegistrarMateriales(List<Material> materiales) {

            SqlCommand cmd = null;
            try
            {
                conn.Open();
                for (int i = 0; i < materiales.Count; i++)
                {
                    Material it = materiales[i];
                    if (it != null)
                    {
                        //reglas de mapeo; Ciro Palomino Almanza; 20190719
                        //1.- Si la unidad de venta es igual a la unidad de consumo que es la unidad base de SAP
                        if (it.UnidadVenta == null) it.UnidadVenta = it.UnidadConsumo;

                        cmd = new SqlCommand("sp_sap_inarticulos", conn);// Procedimiento ingresa o actualiza Material recibido
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@carticulos_id", it.CodMaterial));
                        cmd.Parameters.Add(new SqlParameter("@carticulos_nombre", it.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@cunidad_de_consumo", it.UnidadConsumo));
                        cmd.Parameters.Add(new SqlParameter("@narticulos_factor_conversion", it.FactorConversion));
                        cmd.Parameters.Add(new SqlParameter("@carticulos_estado", it.Estado));
                        cmd.Parameters.Add(new SqlParameter("@cunidad_de_medida_venta", it.UnidadVenta));
                        cmd.Parameters.Add(new SqlParameter("@nfactor_a_venta", it.FactorConversionVenta));
                        cmd.Parameters.Add(new SqlParameter("@ccategoria_2", it.CCategoria_2));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally {

                if (conn.State == System.Data.ConnectionState.Open) {
                    conn.Close();
                }
            }
        }

        public void RegistrarVendedores(List<Vendedor> vendedores) {
            SqlCommand cmd = null;
            try
            {
                conn.Open();
                for (int i = 0; i < vendedores.Count; i++)
                {
                    Vendedor it = vendedores[i];
                    cmd = new SqlCommand("pWSvendedor", conn);// Procedimiento ingresa o actualiza Material recibido
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (it.IdVendedor != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cid_vend", it.IdVendedor));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cid_vend",""));
                    }
                   

                    cmd.Parameters.Add(new SqlParameter("@cnomb_vend", it.Nombre));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                    e.ToString();
            }
            finally
            {

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

    }

}