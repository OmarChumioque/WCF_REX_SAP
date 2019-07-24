Declare @KUNNR char(10),
@BUkRS char(4),
@BELNR_D char(10),
@GJAHR numeric(10,4),
@BUZEI numeric(10,4),
@BUDAT datetime,
@DZUONR char(18),
@XBLNR1 char(16),
@BLART char(2),
@SHKZG char(1),
@WAERS char(5),
@DMBTR numeric(13,2),
@WRBTR numeric(13,2),
@DZFBDT datetime,
@DATS datetime,
@UZEIT char(10)


declare @cld varchar(30),@ct_doc CHAR(2),@cserie CHAR(4),@cdoc CHAR(4),@cmoneda char(1),
@cid_oficina char(6)

Declare CurCobranza Cursor for
Select TOP 10 KUNNR,BUkRS,BELNR_D,GJAHR,BUZEI,BUDAT,
DZUONR,XBLNR1,BLART,SHKZG,WAERS,DMBTR,WRBTR,
DZFBDT,DATS,UZEIT
FROM DocSAPCobranzaRec 

OPEN CurCobranza
FETCH NEXT FROM CurCobranza INTO @KUNNR,
@BUkRS,@BELNR_D,@GJAHR,
@BUZEI,@BUDAT,@DZUONR,
@XBLNR1,@BLART,@SHKZG,
@WAERS,@DMBTR,@WRBTR,
@DZFBDT,@DATS,@UZEIT

--print right('0000012345',5)
--print substring('01-00031-1518262',5,4)

WHILE @@FETCH_STATUS = 0
BEGIN
	Exec sp_appidsbdmain 'tsaldos_ctacte.cid_ctacte',10,@cld output
--	SET @ct_doc=(Case WHEN substring(@XBLNR1,1,2)='01'
--				then 'FA' 
--				When substring(@XBLNR1,1,2)='03' then 'BO' END )

	SET @ct_doc=Substring(@XBLNR1,1,2)
	SET @cserie=substring(@XBLNR1,5,4)
	SET @cdoc= substring(@XBLNR1,10,7)
	set  @cmoneda=(case when @WAERS='PEN' THEN 'N' ELSE 'D' END) 

	INSERT tsaldos_ctacte
	( cid_ctacte,
	  ct_anltc,
	  canltc,
	  ct_doc,
	  cserie,
	  cdoc,
	  cmoneda,
	  nmonto,
	  cid_oficina,
	  df_emis,
	  cuser_creacion)
	  values
	  (@cld,
	  'CL',
	  right(@KUNNR,8),
	  @ct_doc,
	  @cserie,
	  @cdoc,
	  @cmoneda,
	  @WRBTR,
	  '000001',
	  @DZFBDT,
	  'SAPYiCHANG')

	  SET @ct_doc=(Case WHEN substring(@XBLNR1,1,2)='01'
				then 'FA' 
				When substring(@XBLNR1,1,2)='03' then 'BO' END )


	  INSERT _movimiento_ventas
	  (cid_ofi,
	  nid_cia,
	  ctipo_documento_id,
	  cserie_documento,
	  cnumero_documento,
	  cid_clie,
	  cid_vend,
	  dfecha_documento,
	  cid_moneda,
	  nimporte_neto)
	  Values
	  ('01',
	   '1',
	   @ct_doc,
	   @cserie,
	   @cdoc,
	   right(@KUNNR,8),
	   '000001',
	   @DZFBDT,
	   @cmoneda,
	   @WRBTR)

	  FETCH NEXT FROM CurCobranza INTO @KUNNR,
	  @BUkRS,@BELNR_D,@GJAHR,
	  @BUZEI,@BUDAT,@DZUONR,
	  @XBLNR1,@BLART,@SHKZG,
	  @WAERS,@DMBTR,@WRBTR,
      @DZFBDT,@DATS,@UZEIT

End
close CurCobranza
deallocate CurCobranza
