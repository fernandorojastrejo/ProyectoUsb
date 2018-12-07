create PROCEDURE [dbo].[InsertaProductoPrecio] 
	@ProductoId int,
	@Precio1	float,
	@Precio2	float,
	@Precio3 float
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO cat.Precio(ProductoId, Precio1, Precio2, Precio3, Mes, ayo, Emitio)
      VALUES (@ProductoId, @Precio1, @Precio2, @Precio3, NULL, NULL, NULL)
END