create PROCEDURE [dbo].[ActualizaProductoPrecio] 
	@ProductoId int,
	@Precio1	float,
	@Precio2	float,
	@Precio3 float
AS
BEGIN
      SET NOCOUNT ON;
      UPDATE cat.Precio SET 
	   Precio1 = @Precio1, Precio2 = @Precio2, Precio3 = @Precio3 
      WHERE ProductoId = @ProductoId
END