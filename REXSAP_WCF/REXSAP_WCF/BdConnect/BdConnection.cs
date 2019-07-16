using REXSAP_WCF.Model;
using System;
using System.Collections.Generic;
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
            /*
            conn = new SqlConnection("Data Source=185.144.157.97;" +
                "Initial Catalog=Rex;" +
                "User Id=omarch1409;Password=1409Chumioque;" +
                "connect timeout=2000;");
             */


            conn = new SqlConnection("Data Source=172.31.236.221;" +
               "Initial Catalog=rex;" +
               "User Id=rexdb;Password=rexdb2019;" +
               "connect timeout=2000;");

        }

        public void RegistrarClientes(List<Cliente> clientes) {
            SqlCommand cmd = null;
            try
            {
                conn.Open();

                for (int i = 0; i < clientes.Count; i++)
                {
                    Cliente it = clientes[i];
                    cmd = new SqlCommand("pws_cliente", conn);// Procedimiento ingresa o actualiza cliente recibido
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
                        cmd.Parameters.Add(new SqlParameter("@cubigeo_clie",""));
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
                    if (it.Estado != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@cid_stat_clie", it.Estado));
                    }
                    else {
                        cmd.Parameters.Add(new SqlParameter("@cid_stat_clie", ""));
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
                    cmd.Parameters.Add(new SqlParameter("@cproveedor_id",it.IdProveedor));
                    cmd.Parameters.Add(new SqlParameter("@cproveedor_nombre",it.Nombre));
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
                        cmd = new SqlCommand("pWSarticulos", conn);// Procedimiento ingresa o actualiza Material recibido
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@carticulos_id", it.CodMaterial));
                        cmd.Parameters.Add(new SqlParameter("@carticulos_nombre", it.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@cunidad_de_consumo", it.UnidadConsumo));
                        cmd.Parameters.Add(new SqlParameter("@narticulos_factor_conversion", it.FactorConversion));
                        cmd.Parameters.Add(new SqlParameter("@carticulos_estado", it.Estado));
                        cmd.Parameters.Add(new SqlParameter("@cunidad_de_medida_venta", it.UnidadVenta));
                        cmd.Parameters.Add(new SqlParameter("@nfactor_a_venta", it.FactorConversionVenta));
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
                    cmd.Parameters.Add(new SqlParameter("@cid_vend", it.IdVendedor));
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