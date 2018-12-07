CREATE PROCEDURE [dbo].[InsertaProductoExistencia] 
	@ProductoId int,
	@Minimo	int,
	@Maximo	int,
	@Existente int,
	@Reservado	int,
	@FechaArribo datetime
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO ProductoExistencia(ProductoId, Minimo, Maximo, Existente, Reservado, FechaArribo)
      VALUES (@ProductoId, @Minimo, @Maximo, @Existente, @Reservado, @FechaArribo)
END