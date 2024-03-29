USE [Rex]
GO
/****** Object:  StoredProcedure [dbo].[pIntPedido]    Script Date: 24/07/2019 7:43:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[pIntPedido] 
@fechaPedido datetime
as
begin
	

	SELECT RTRIM(A.ctipo_documento_id)+RTRIM(A.cserie_documento)+RTRIM(A.cnumero_documento)+A.cnumero_documento_item  AS BSTKD, 
	 '00'+ B.cid_clie KUNNR, 
	 B.dfecha_documento AUDAT,_forma_pago.ccodigo_forma_SAP ZTERM,
	  Case When _cliente.ctipo_documento_habitual='FA' then 'ZREF' Else 'ZRBL' End
	  AS AUART, Case WHEN cid_MONEDA='N' then 'PEN' Else 'USD' End  AS WAERK,
	 A.NIMPORTE_BRUTO NETWR,'0000000000'+ rtrim(A.carticulos_id) MATNR, 
	A.ncantidad_pedido KWMENG, A.nprecio_unitario NETPR, 0.00 KBETRK, 
	 ' ' AS VBELN,case when lgratuito=0 then 'ZTAN' WHEN lgratuito=1 THEN 'ZTNN' end PSTYV
	, case dfecha_entrega 
	when '1900-01-01 00:00:00' then B.dfecha_documento
	when  null  then dfecha_documento
	else dfecha_entrega
	end
	as KETDAT,A.cnumero_documento_item POSNR,
	A.cunidad_de_venta VRKME
	FROM dbo._movimiento_ventas_detalle  A INNER JOIN
	 dbo._movimiento_ventas B ON A.ctipo_documento_id = B.ctipo_documento_id AND 
	  A.cserie_documento = B.cserie_documento AND A.cnumero_documento = B.cnumero_documento INNER JOIN
                         dbo._forma_pago ON B.cforma_pago_id = dbo._forma_pago.cforma_pago_id INNER JOIN
                         dbo._cliente ON B.cid_clie = dbo._cliente.cid_clie
	WHERE  dfecha_documento=DATEADD(dd, DATEDIFF(dd, 0, @fechaPedido), 0)
	and B.ctipo_documento_id = 'PE' and B.cestado_documento_id = 'I' 
	ORDER BY RTRIM(A.ctipo_documento_id)+RTRIM(A.cserie_documento)+RTRIM(A.cnumero_documento)+A.cnumero_documento_item 
	
	Declare CurPedido  Cursor for
	Select BSTKD,	VBELN from DocSapPedidos

	Open CurPedido
	FETCH NEXT FROM CurPedido INTO @BSTKD,	@VBELN
	WHILE @@FETCH_STATUS = 0
	BEGIN
		set @ctipo_documento_id=substring(@BSTKD,1,2)
		set @cserie_documento= substring(@BSTKD,3,3)
		set @cnumero_documento= substring(@BSTKD,6,8)

		update _movimiento_ventas
		set mobservaciones=@VBELN,
		cestado_documento_id='E'
		Where  ctipo_documento_id= rtrim( @ctipo_documento_id ) and
		cserie_documento=rtrim(@cserie_documento) and
		cnumero_documento=rtrim(@cnumero_documento)
		
		FETCH NEXT FROM CurPedido INTO @BSTKD,	@VBELN

	END
	Close CurPedido
	Deallocate CurPedido
end


go

