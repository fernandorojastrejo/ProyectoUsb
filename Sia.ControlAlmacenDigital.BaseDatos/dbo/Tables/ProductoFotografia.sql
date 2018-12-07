CREATE TABLE [dbo].[ProductoFotografia] (
    [ProductoFotoId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ProductoId]     INT            NOT NULL,
    [UrlImagen]      NVARCHAR (500) NOT NULL,
    CONSTRAINT [FK_ProductoFotografia_Producto] FOREIGN KEY ([ProductoId]) REFERENCES [cat].[Producto] ([ProductoId])
);

