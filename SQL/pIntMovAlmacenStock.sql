USE [Rex]
GO
/****** Object:  StoredProcedure [dbo].[pIntmovalmacenstock]    Script Date: 24/07/2019 10:40:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[pIntmovalmacenstock]
as
Declare 
@DOCEJEM char(18),
@WERKS char(4),
@LGORT char(4),
@MATNR char(18),
@VKUML numeric(15,2),
@MEINS char(3) ,
@ZDATE datetime

Declare @nro_reg int, @cld varchar(30), @lcnumero_documento char(11), @lcDOCEJEM char(18),@lcid char(14)
Declare @cmovimiento_id char(8),@nitem integer ,@ndoc integer,@error_msg varchar(200)
--Generar cabecera
--			exec sp_appidsbdmain 'MOVIMIENTO.CMOVIMIENTO_ID',8,@cld output
---Genera los documentos de Ingreso---
-----
-------Se agrego eliminacion d movimientos a la  carga de stock----
Delete _movimiento_detalle
Delete _movimiento
------
Delete _movimiento_detalle where cmovimiento_nro_documento 
in (select cmovimiento_nro_documento from _movimiento
Where cmovimiento_doc_referencia like'001SALDOSAP%')

Delete _movimiento
Where cmovimiento_doc_referencia like'001SALDOSAP%'

set @ndoc=1

Declare CurSalida SCROLL Cursor for
Select '001SALDOSAP'+ltrim(cast(@ndoc as char(5))) as DOCEJEM ,WERKS ,LGORT ,MATNR ,VKUML ,MEINS ,ZDATE 
from docSapStock  ----colocar filtro de fecha 
order by 1

Open CurSalida
FETCH NEXT FROM CurSalida INTO @DOCEJEM,
@WERKS ,
@LGORT ,
@MATNR ,
@VKUML ,
@MEINS ,
@ZDATE 

set @lcDOCEJEM=@DOCEJEM
set @nitem=1

if isnull(@lcDOCEJEM,'') <>'' 
	begin	
		exec sp_crea_cabecera_kardex '001',@lcnumero_documento output
	--	print @lcnumero_documento
		Update _movimiento 
		set cncia='1',
		cmovimiento_tipo='I',
		calmacenes_origen_id='001',
		ctransaccion_almacen_id='0012',
		cmovimiento_doc_referencia=@DOCEJEM,
		dmovimiento_fechahora=@ZDATE,
		coficina_id='01',
		cmovimiento_estado='Ad'
		Where cmovimiento_nro_documento=rtrim(@lcnumero_documento)
	END

FETCH FIRST FROM CurSalida INTO @DOCEJEM,
@WERKS ,
@LGORT ,
@MATNR ,
@VKUML ,
@MEINS ,
@ZDATE    

WHILE @@FETCH_STATUS = 0
BEGIN
	print ltrim(cast(@ndoc as char(5)))
	if @nitem>899
		begin
			set @ndoc=@ndoc+1
--			print '---'+ltrim(cast(@ndoc as char(5)))
			set @DOCEJEM='001SALDOSAP'+ltrim(cast(@ndoc as char(5)))
			set @nitem=1
		END	
	else
		set @DOCEJEM='001SALDOSAP'+ltrim(cast(@ndoc as char(5)))
	
	if @DOCEJEM=@lcDOCEJEM
		begin
			--print right('0000'+rtrim(cast(1 as char(3))),3)
			exec sp_appidsbdmain 'movimiento_detalle.cmovimiento_detalle_id',8,@lcid output 
			set @cmovimiento_id=isnull((Select cmovimiento_id
			from  _movimiento where cmovimiento_nro_documento=rtrim(@lcnumero_documento)),'')
			Insert _movimiento_detalle
			(cmovimiento_id,
			cmovimiento_detalle_id,
			cmovimiento_nro_documento,
			carticulos_id,
			cmovimiento_detalle_item,
			cunidad_medida,
			nmovimiento_detalle_cantidad,
			mmovimiento_detalle_observacio,
			cmovimiento_detalle_estado)
			Values
			(@cmovimiento_id,
			@lcid,
			@lcnumero_documento,
			right(rtrim(@MATNR),8),
			right('0000'+rtrim(cast(@nitem as char(3))),3),
			@MEINS,
			@VKUML,
			'*',
			'Ad')					
	--		print 'detalle '+@lcDOCEJEM 
			FETCH NEXT FROM CurSalida INTO @DOCEJEM,
			@WERKS ,
			@LGORT ,
			@MATNR ,
			@VKUML ,
			@MEINS ,
			@ZDATE    
			set @nitem=@nitem+1			  
		End
	Else
		begin	
			exec sp_crea_cabecera_kardex '001',@lcnumero_documento output
	--		print @lcnumero_documento

			set @lcDOCEJEM=@DOCEJEM

			Update _movimiento 
			set cncia='1',
			cmovimiento_tipo='I',
			calmacenes_origen_id='001',
			ctransaccion_almacen_id='0012',
			cmovimiento_doc_referencia=@DOCEJEM,
			dmovimiento_fechahora=@ZDATE,
			coficina_id='01',
			cmovimiento_estado='Ad'
			Where cmovimiento_nro_documento=rtrim(@lcnumero_documento)
			set @nitem=1
		End
End
Close CurSalida
Deallocate CurSalida
--Actualiza stock almacen
exec sp_valida_stock @error_msg output
