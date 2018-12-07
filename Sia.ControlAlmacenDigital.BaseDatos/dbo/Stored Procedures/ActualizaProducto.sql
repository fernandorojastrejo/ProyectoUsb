CREATE PROCEDURE [dbo].[ActualizaProducto] 
	@ProductoId int,
	@CategoriaId int,
	@Codigo	nvarchar(128),
	@CodigoBarra	nvarchar(128) = null,
	@Nombre nvarchar(150),
	@Pieza	int,
	@Descripcion	nvarchar(500),
	@Color	nvarchar(128) = null,
	@Capacidad	nvarchar(128) = null,
	@Material	nvarchar(250) = null,
	@Activo	bit,
	@Medida nvarchar(128) = null,
	@EsPrincipal bit,
	@CodigoPrincipal nvarchar(128) = null
AS
BEGIN
      SET NOCOUNT ON;
      update cat.Producto SET
	  CategoriaId = @CategoriaId, Codigo = @Codigo, CodigoBarra = @CodigoBarra, Nombre = @Nombre, 
	  Pieza = @Pieza, Descripcion = @Descripcion, Color = @Color, Capacidad = @Capacidad, Material = @Material, Activo = @Activo,
	  Medida = @Medida, EsPrincipal = @EsPrincipal, CodigoPrincipal = @CodigoPrincipal 
	  where ProductoId = @ProductoId
END