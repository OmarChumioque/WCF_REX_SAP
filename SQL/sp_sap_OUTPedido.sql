USE [rex]
GO
/****** Object:  StoredProcedure [dbo].[sp_sap_OUTPedido]    Script Date: 25/07/2019 10:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_sap_OUTPedido]
  AS
  Declare CurPedido  Cursor for
	Select BSTKD,	VBELN from DocSapPedidos

		Declare @BSTKD	char(35),
			@VBELN	char(10)	
	Declare  @ctipo_documento_id char(2), @cserie_documento char(3), @cnumero_documento char(8)

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