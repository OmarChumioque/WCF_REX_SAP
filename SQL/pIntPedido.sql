USE [rex]
GO
/****** Object:  StoredProcedure [dbo].[pIntPedido]    Script Date: 24/07/2019 07:00:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


		ALTER procedure [dbo].[pIntPedido] 
		@fechaPedido datetime
		as
		begin
			/*
			SELECT RTRIM(A.ctipo_documento_id)+RTRIM(A.cserie_documento)+RTRIM(A.cnumero_documento) AS BSTKD, 
			 B.cid_clie KUNNR, 
			 B.dfecha_documento AUDAT,B.cforma_pago_id ZTERM, '01' AS AUART, '01' AS WAERK,
			 B.NIMPORTE_BRUTO NETWR,A.carticulos_id MATNR, 
			A.ncantidad_pedido KWMENG, A.nprecio_unitario NETPR, A.nporc_descuento KBETRK, 
			A.cnumero_documento_item PSTYV, dFecha_entrega AS VBELN
			FROM dbo._movimiento_ventas_detalle  A INNER JOIN
			 dbo._movimiento_ventas B ON A.ctipo_documento_id = B.ctipo_documento_id AND 
			  A.cserie_documento = B.cserie_documento AND A.cnumero_documento = B.cnumero_documento
			WHERE        (A.cnumero_documento = '00035039')
			*/

			/*
			SELECT RTRIM(A.ctipo_documento_id)+RTRIM(A.cserie_documento)+RTRIM(A.cnumero_documento)+A.cnumero_documento_item  AS BSTKD, 
			 '00'+ B.cid_clie KUNNR, 
			 B.dfecha_documento AUDAT,'D017' ZTERM, 'ZRBL' AS AUART, 'PEN' AS WAERK,
			 A.NIMPORTE_BRUTO NETWR,'0000000000'+ rtrim(A.carticulos_id) MATNR, 
			A.ncantidad_pedido KWMENG, A.nprecio_unitario NETPR, 0.00 KBETRK, 
			 ' ' AS VBELN,case when lgratuito=0 then 'ZTAN' WHEN lgratuito=1 THEN 'ZTNN' end PSTYV
			,dfecha_entrega as KETDAT,A.cnumero_documento_item POSNR
			FROM dbo._movimiento_ventas_detalle  A INNER JOIN
			 dbo._movimiento_ventas B ON A.ctipo_documento_id = B.ctipo_documento_id AND 
			  A.cserie_documento = B.cserie_documento AND A.cnumero_documento = B.cnumero_documento
			WHERE        (A.cnumero_documento = '00035039') OR    (A.cnumero_documento = '00035038')
			OR    (A.cnumero_documento = '00035037') OR    (A.cnumero_documento = '00035036')
			ORDER BY RTRIM(A.ctipo_documento_id)+RTRIM(A.cserie_documento)+RTRIM(A.cnumero_documento)+A.cnumero_documento_item 

		*/
				SELECT RTRIM(A.ctipo_documento_id)+RTRIM(A.cserie_documento)+RTRIM(A.cnumero_documento)+A.cnumero_documento_item  AS BSTKD, 
			 '00'+ B.cid_clie KUNNR, 
			 B.dfecha_documento AUDAT,'D017' ZTERM, 'ZRBL' AS AUART, 'PEN' AS WAERK,
			 A.NIMPORTE_BRUTO NETWR,'0000000000'+ rtrim(A.carticulos_id) MATNR, 
			A.ncantidad_pedido KWMENG, A.nprecio_unitario NETPR, 0.00 KBETRK, 
			 ' ' AS VBELN,case when lgratuito=0 then 'ZTAN' WHEN lgratuito=1 THEN 'ZTNN' end PSTYV
			,dfecha_entrega as KETDAT,A.cnumero_documento_item POSNR,
			'' VRKME
			FROM dbo._movimiento_ventas_detalle  A INNER JOIN
			 dbo._movimiento_ventas B ON A.ctipo_documento_id = B.ctipo_documento_id AND 
			  A.cserie_documento = B.cserie_documento AND A.cnumero_documento = B.cnumero_documento
			WHERE  dfecha_documento=DATEADD(dd, DATEDIFF(dd, 0, @fechaPedido), 0)
			ORDER BY RTRIM(A.ctipo_documento_id)+RTRIM(A.cserie_documento)+RTRIM(A.cnumero_documento)+A.cnumero_documento_item 

			/*
		print len('000000000000019623')

		OR    (A.cnumero_documento = '00035035')
		OR    (A.cnumero_documento = '00035034')
		OR    (A.cnumero_documento = '00035033')
		OR    (A.cnumero_documento = '00035032') */


		end
