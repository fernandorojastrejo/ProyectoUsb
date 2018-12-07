CREATE TABLE [dbo].[ProductoExistencia] (
    [ProductoExistenciaId] INT      IDENTITY (1, 1) NOT NULL,
    [ProductoId]           INT      NOT NULL,
    [Minimo]               INT      NOT NULL,
    [Maximo]               INT      NOT NULL,
    [Existente]            INT      NOT NULL,
    [Reservado]            INT      NULL,
    [FechaArribo]          DATETIME NULL,
    CONSTRAINT [PK_ProductoExistencia] PRIMARY KEY CLUSTERED ([ProductoExistenciaId] ASC),
    CONSTRAINT [FK_ProductoExistencia_Producto] FOREIGN KEY ([ProductoId]) REFERENCES [cat].[Producto] ([ProductoId])
);







