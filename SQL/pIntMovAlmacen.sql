USE [Rex]
GO
/****** Object:  StoredProcedure [dbo].[pIntmovalmacen]    Script Date: 24/07/2019 10:39:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[pIntmovalmacen] as

Declare @DOCEJEM  char(14),
@BWART    char(3),
@WERKS    char(4),
@LGORT    char(4),
@MATNR    char(18),
@LABST    numeric(13,3),
@MEINS    char(3),
@DATE     datetime,
@TIP      char(1)

Declare @nro_reg int, @cld varchar(30), @lcnumero_documento char(11), @lcDOCEJEM char(14),@lcid char(14)
Declare @cmovimiento_id char(8),@nitem integer, @Error char(100) 
--Generar cabecera
--			exec sp_appidsbdmain 'MOVIMIENTO.CMOVIMIENTO_ID',8,@cld output
---Genera los documentos de Ingreso---
Declare CurSalida SCROLL Cursor for
Select DOCEJEM ,WERKS ,LGORT ,MATNR ,LABST ,MEINS ,DATE,TIP  
from docSap  WHERE  TIP='I'  and  DOCEJEM not in (Select cmovimiento_doc_referencia from _movimiento)----colocar filtro de fecha 
Order by tip,DOCEJEM

Open CurSalida
FETCH NEXT FROM CurSalida INTO @DOCEJEM,
								@WERKS,
								@LGORT,
								@MATNR,
								@LABST,
								@MEINS,
								@DATE,
								@TIP    


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
		dmovimiento_fechahora=@DATE,
		coficina_id='01',
		cmovimiento_estado='Ad'
		Where cmovimiento_nro_documento=rtrim(@lcnumero_documento)
	END

FETCH FIRST FROM CurSalida INTO @DOCEJEM,
								@WERKS,
								@LGORT,
								@MATNR,
								@LABST,
								@MEINS,
								@DATE,
								@TIP    

WHILE @@FETCH_STATUS = 0
BEGIN
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
			@LABST,
			'*',
			'Ad')					
	--		print 'detalle '+@lcDOCEJEM 
			FETCH NEXT FROM CurSalida INTO @DOCEJEM,
											@WERKS,
											@LGORT,
											@MATNR,
											@LABST,
											@MEINS,
											@DATE,
											@TIP 
			set @nitem=@nitem+1			  
		End
	Else
		begin	
			set @lcDOCEJEM=@DOCEJEM		
			exec sp_crea_cabecera_kardex '001',@lcnumero_documento output
	--		print @lcnumero_documento

			set @lcDOCEJEM=@DOCEJEM

			Update _movimiento 
			set cncia='1',
			cmovimiento_tipo='I',
			calmacenes_origen_id='001',
			ctransaccion_almacen_id='0012',
			cmovimiento_doc_referencia=@DOCEJEM,
			dmovimiento_fechahora=@DATE,
			coficina_id='01',
			cmovimiento_estado='Ad'
			Where cmovimiento_nro_documento=rtrim(@lcnumero_documento)
			set @nitem=1
		End
End
Close CurSalida
Deallocate CurSalida


---Genera los documentos de Salida---
Declare CurSalida SCROLL Cursor for
Select DOCEJEM ,WERKS ,LGORT ,MATNR ,LABST ,MEINS ,DATE,TIP  
from docSap  WHERE  TIP='S'  and  DOCEJEM not in 
(Select cmovimiento_doc_referencia from _movimiento)----colocar filtro de fecha 
Order by tip,DOCEJEM

Open CurSalida
FETCH NEXT FROM CurSalida INTO @DOCEJEM,
								@WERKS,
								@LGORT,
								@MATNR,
								@LABST,
								@MEINS,
								@DATE,
								@TIP    


set @lcDOCEJEM=@DOCEJEM
set @nitem=1

if isnull(@lcDOCEJEM,'') <>'' 
	begin	
		exec sp_crea_cabecera_kardex '001',@lcnumero_documento output
	--	print @lcnumero_documento
		Update _movimiento 
		set cncia='1',
		cmovimiento_tipo='S',
		calmacenes_origen_id='001',
		ctransaccion_almacen_id='0002',
		cmovimiento_doc_referencia=@DOCEJEM,
		dmovimiento_fechahora=@DATE,
		coficina_id='01',
		cmovimiento_estado='Ad'
		Where cmovimiento_nro_documento=rtrim(@lcnumero_documento)
	END

FETCH FIRST FROM CurSalida INTO @DOCEJEM,
								@WERKS,
								@LGORT,
								@MATNR,
								@LABST,
								@MEINS,
								@DATE,
								@TIP    

WHILE @@FETCH_STATUS = 0
BEGIN
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
			@LABST,
			'*',
			'Ad')					
--			print 'detalle '+@lcDOCEJEM 
			FETCH NEXT FROM CurSalida INTO @DOCEJEM,
											@WERKS,
											@LGORT,
											@MATNR,
											@LABST,
											@MEINS,
											@DATE,
											@TIP 
			set @nitem=@nitem+1			  
		End
	Else
		begin	
			set @lcDOCEJEM=@DOCEJEM		
			exec sp_crea_cabecera_kardex '001',@lcnumero_documento output
	--		print @lcnumero_documento

			set @lcDOCEJEM=@DOCEJEM

			Update _movimiento 
			set cncia='1',
			cmovimiento_tipo='S',
			calmacenes_origen_id='001',
			ctransaccion_almacen_id='0002',
			cmovimiento_doc_referencia=@DOCEJEM,
			dmovimiento_fechahora=@DATE,
			coficina_id='01',
			cmovimiento_estado='Ad'
			Where cmovimiento_nro_documento=rtrim(@lcnumero_documento)
			set @nitem=1
		End
End
Close CurSalida
Deallocate CurSalida
---actualiza stock
exec sp_valida_stock @Error output 

