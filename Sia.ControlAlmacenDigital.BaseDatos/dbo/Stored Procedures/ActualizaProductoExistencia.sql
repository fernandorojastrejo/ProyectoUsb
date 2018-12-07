CREATE PROCEDURE [dbo].[ActualizaProductoExistencia] 
	@ProductoId int,
	@Minimo	int,
	@Maximo	int,
	@Existente int,
	@Reservado	int,
	@FechaArribo datetime
AS
BEGIN
      SET NOCOUNT ON;
      UPDATE ProductoExistencia SET 
	   Minimo = @Minimo, Maximo = @Maximo, Existente = @Existente, Reservado = @Reservado, FechaArribo = @FechaArribo
      WHERE ProductoId = @ProductoId
END