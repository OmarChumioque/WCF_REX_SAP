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
            conn = new SqlConnection("Data Source=185.144.157.97;" +
                "Initial Catalog=Rex;" +
                "User Id=omarch1409;Password=1409Chumioque;" +
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
                    cmd.Parameters.Add(new SqlParameter("@ccod_clie", it.Codigo));
                    cmd.Parameters.Add(new SqlParameter("@cnomb_clie", it.Denominacion));
                    cmd.Parameters.Add(new SqlParameter("@cruc_clie", it.NumRuc));
                    cmd.Parameters.Add(new SqlParameter("@mdirec_clie", it.Direccion));
                    cmd.Parameters.Add(new SqlParameter("@cubigeo_clie", it.CodUbigeo));
                    cmd.Parameters.Add(new SqlParameter("@cid_vend", it.CodVendedor));
                    cmd.Parameters.Add(new SqlParameter("@cid_grupclie", it.GrupDescuento));
                    cmd.Parameters.Add(new SqlParameter("@cid_stat_clie", it.Estado));
                    cmd.Parameters.Add(new SqlParameter("@cforma_pago_id", it.CodFormaPago));
                    cmd.Parameters.Add(new SqlParameter("@cgiro_id", it.CodCanalCliente));
                    cmd.Parameters.Add(new SqlParameter("@ctipo_documento_habitual", it.TipoDoc));
                    cmd.Parameters.Add(new SqlParameter("@cdocumento_dni", it.NumDni));
                    cmd.Parameters.Add(new SqlParameter("@a_paterno", it.ApellidoPat));
                    cmd.Parameters.Add(new SqlParameter("@a_materno", it.ApellidoMat));
                    cmd.Parameters.Add(new SqlParameter("@nombres", it.NombresCliente));
                    cmd.Parameters.Add(new SqlParameter("@cnombre_comercial", it.NombreComercialNegocio));
                    cmd.Parameters.Add(new SqlParameter("@cubigeo_env", it.CodUbigeoEnv));
                    cmd.Parameters.Add(new SqlParameter("@mdire_env", it.DireccionEntregaPedido));
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