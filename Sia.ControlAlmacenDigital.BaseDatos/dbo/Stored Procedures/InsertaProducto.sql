CREATE PROCEDURE [dbo].[InsertaProducto] 
	
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
	@CodigoPrincipal nvarchar(128) = null,
	@id int output 
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO cat.Producto(CategoriaId, Codigo, CodigoBarra, Nombre, Pieza, Descripcion, Color, Capacidad, Material, Activo, Medida, EsPrincipal, CodigoPrincipal)
      VALUES (@CategoriaId, @Codigo, @CodigoBarra, @Nombre, @Pieza, @Descripcion, @Color, @Capacidad, @Material, @Activo, @Medida, @EsPrincipal, @CodigoPrincipal)
      SET @id=SCOPE_IDENTITY()
      RETURN  @id
END