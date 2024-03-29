USE [rex]
GO
/****** Object:  StoredProcedure [dbo].[sp_sap_INPedido]    Script Date: 24/07/2019 12:33:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_sap_INPedido] 
AS 
  BEGIN 
      DECLARE @fechaPedido DATETIME = Getdate(), 
              @vendedor    VARCHAR(6) 

      --Obtengo la venta de un solo vendedor 
      SELECT TOP 1 @vendedor = cid_vend 
      FROM   _movimiento_ventas 
      WHERE  cestado_documento_id <> 'E' 
             AND Isnull(Cast(mobservaciones AS VARCHAR(25)), '') = '' 
             AND dfecha_documento BETWEEN @fechaPedido - 3 AND @fechaPedido + 1 

			 print @vendedor
      SELECT Rtrim(D.ctipo_documento_id) 
             + Rtrim(D.cserie_documento) 
             + Rtrim(D.cnumero_documento) 
             + D.cnumero_documento_item            AS BSTKD, 
             '00' + C.cid_clie                     AS KUNNR, 
             C.dfecha_documento                    AS AUDAT, 
             Fp.ccodigo_forma_sap                  AS ZTERM, 
             CASE 
               WHEN Cl.ctipo_documento_habitual = 'FA' THEN 'ZRFA' 
               ELSE 'ZRBL' 
             END                                   AS AUART, 
             CASE 
               WHEN cid_moneda = 'N' THEN 'PEN' 
               ELSE 'USD' 
             END                                   AS WAERK, 
             D.nimporte_bruto                      AS NETWR, 
             '0000000000' + Rtrim(D.carticulos_id) AS MATNR, 
             D.ncantidad_pedido                    AS KWMENG, 
             D.nprecio_unitario                    AS NETPR, 
             D.nimporte_descuento                  AS KBETRK, 
             ' '                                   AS VBELN, 
             CASE 
               WHEN lgratuito = 0 THEN 'ZTAN' 
               WHEN lgratuito = 1 THEN 'ZTNN' 
             END                                   AS PSTYV, 
             CASE dfecha_entrega 
               WHEN '1900-01-01 00:00:00' THEN c.dfecha_documento 
               WHEN NULL THEN dfecha_documento 
               ELSE dfecha_entrega 
             END                                   AS KETDAT, 
             D.cnumero_documento_item              AS POSNR, 
             D.cunidad_de_venta                    AS VRKME 
      FROM   _movimiento_ventas_detalle AS D 
             INNER JOIN _movimiento_ventas AS C 
                     ON D.ctipo_documento_id = C.ctipo_documento_id 
                        AND D.cserie_documento = C.cserie_documento 
                        AND D.cnumero_documento = C.cnumero_documento 
             INNER JOIN _forma_pago AS Fp 
                     ON C.cforma_pago_id = Fp.cforma_pago_id 
             INNER JOIN _cliente AS Cl 
                     ON C.cid_clie = Cl.cid_clie 
      WHERE  ( C.cid_vend = @vendedor ) 
             AND ( C.cestado_documento_id <> 'E' ) 
             AND ( Isnull(Cast(C.mobservaciones AS VARCHAR(25)), '') = '' ) 
             AND ( C.dfecha_documento BETWEEN @vendedor - 3 AND @fechaPedido + 1) 
      ORDER  BY bstkd 
  END 

--exec Sp_sap_inpedido