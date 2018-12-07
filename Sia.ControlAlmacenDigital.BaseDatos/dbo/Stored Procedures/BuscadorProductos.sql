-- =============================================
-- Author:		<Benjamin Ramirez>
-- Create date: <09/10/18>
-- Description:	<Buscador de productos con multiples parametros>
-- =============================================
CREATE PROCEDURE [dbo].[BuscadorProductos] 
	-- Add the parameters for the stored procedure here
	@CategoriaId int,
	@Descripcion varchar (100) = NULL,
	@Color varchar (100) = NULL,
	@Capacidad varchar (100) = NULL,
	@Material varchar (100) = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--SELECT 
	--ProductoId
 --   ,CategoriaId
 --   ,Codigo
 --   ,CodigoBarra
 --   ,Nombre
 --   ,Pieza
 --   ,Descripcion
 --   ,Color
 --   ,Capacidad
 --   ,Material
	--  FROM cat.Producto
	--  where CategoriaId = @CategoriaId
	--  AND Descripcion like '%'+ @Descripcion+'%'
	-- AND Color like '%'+@Color+'%'
	--  AND Capacidad like '%'+@Capacidad+'%'
	--  AND Material like '%'+@Material+'%'
 --   -- Insert statements for procedure here
	IF (@Color is not NULL AND @Capacidad is not NULL AND @Material is not NULL)
	BEGIN
	SELECT 
	ProductoId
    ,CategoriaId
    ,Codigo
    ,CodigoBarra
    ,Nombre
    ,Pieza
    ,Descripcion
    ,Color
    ,Capacidad
    ,Material
	  FROM cat.Producto
	  where CategoriaId = @CategoriaId
	  AND Descripcion like '%'+ @Descripcion+'%'
	 AND Color like '%'+@Color+'%'
	  AND Capacidad like '%'+@Capacidad+'%'
	  AND Material like '%'+@Material+'%'
	  order by Descripcion asc
	END

	IF(@Color is NULL AND @Capacidad is not NULL AND @Material is not NULL)
	BEGIN
	SELECT 
	ProductoId
    ,CategoriaId
    ,Codigo
    ,CodigoBarra
    ,Nombre
    ,Pieza
    ,Descripcion
    ,Color
    ,Capacidad
    ,Material
	  FROM cat.Producto
	  where CategoriaId = @CategoriaId
	  AND Descripcion like '%'+ @Descripcion+'%'
	  AND Capacidad like '%'+@Capacidad+'%'
	  AND Material like '%'+@Material+'%'
	  order by Descripcion asc
	END

	IF(@Color is NULL AND @Capacidad is NULL AND @Material is not NULL)
	BEGIN
	SELECT 
	ProductoId
    ,CategoriaId
    ,Codigo
    ,CodigoBarra
    ,Nombre
    ,Pieza
    ,Descripcion
    ,Color
    ,Capacidad
    ,Material
	  FROM cat.Producto
	  where CategoriaId = @CategoriaId
	  AND Descripcion like '%'+ @Descripcion+'%'
	  AND Material like '%'+@Material+'%'
	  order by Descripcion asc
	END

	IF(@Color is NULL AND @Capacidad is NULL AND @Material is NULL)
	BEGIN
	SELECT 
	ProductoId
    ,CategoriaId
    ,Codigo
    ,CodigoBarra
    ,Nombre
    ,Pieza
    ,Descripcion
    ,Color
    ,Capacidad
    ,Material
	  FROM cat.Producto
	  where CategoriaId = @CategoriaId
	  AND Descripcion like '%'+ @Descripcion+'%'
	  order by Descripcion asc
	END

	IF(@Color is NULL AND @Capacidad is not NULL AND @Material is NULL)
	BEGIN
	SELECT 
	ProductoId
    ,CategoriaId
    ,Codigo
    ,CodigoBarra
    ,Nombre
    ,Pieza
    ,Descripcion
    ,Color
    ,Capacidad
    ,Material
	  FROM cat.Producto
	  where CategoriaId = @CategoriaId
	  AND Descripcion like '%'+ @Descripcion+'%'
	  AND Capacidad like '%'+@Capacidad+'%'
	  order by Descripcion asc
	END

	IF(@Color is NULL AND @Capacidad is NULL AND @Material is not NULL)
	BEGIN
	SELECT 
	ProductoId
    ,CategoriaId
    ,Codigo
    ,CodigoBarra
    ,Nombre
    ,Pieza
    ,Descripcion
    ,Color
    ,Capacidad
    ,Material
	  FROM cat.Producto
	  where CategoriaId = @CategoriaId
	  AND Descripcion like '%'+ @Descripcion+'%'
	  AND Material like '%'+@Material+'%'
	  order by Descripcion asc
	END

	IF(@Color is not NULL AND @Capacidad is NULL AND @Material is NULL)
	BEGIN
	SELECT 
	ProductoId
    ,CategoriaId
    ,Codigo
    ,CodigoBarra
    ,Nombre
    ,Pieza
    ,Descripcion
    ,Color
    ,Capacidad
    ,Material
	  FROM cat.Producto
	  where CategoriaId = @CategoriaId
	  AND Descripcion like '%'+ @Descripcion+'%'
	  AND Color like '%'+@Color+'%'
	  order by Descripcion asc
	END

		IF(@Color is not NULL AND @Capacidad is not NULL AND @Material is NULL)
	BEGIN
	SELECT 
	ProductoId
    ,CategoriaId
    ,Codigo
    ,CodigoBarra
    ,Nombre
    ,Pieza
    ,Descripcion
    ,Color
    ,Capacidad
    ,Material
	  FROM cat.Producto
	  where CategoriaId = @CategoriaId
	  AND Descripcion like '%'+ @Descripcion+'%'
	  AND Color like '%'+@Color+'%'
	  AND Capacidad like '%'+@Capacidad+'%'
	  order by Descripcion asc
	END

	IF(@Color is not NULL AND @Capacidad is NULL AND @Material is not NULL)
	BEGIN
	SELECT 
	ProductoId
    ,CategoriaId
    ,Codigo
    ,CodigoBarra
    ,Nombre
    ,Pieza
    ,Descripcion
    ,Color
    ,Capacidad
    ,Material
	  FROM cat.Producto
	  where CategoriaId = @CategoriaId
	  AND Descripcion like '%'+ @Descripcion+'%'
	  AND Color like '%'+@Color+'%'
	  AND Material like '%'+@Material+'%'
	  order by Descripcion asc
	END
	

	
END
GO

