CREATE TABLE [cat].[Precio] (
    [PrecioId]   INT        IDENTITY (1, 1) NOT NULL,
    [ProductoId] INT        NOT NULL,
    [Precio1]    FLOAT (53) NOT NULL,
    [Precio2]    FLOAT (53) NULL,
    [Precio3]    FLOAT (53) NULL,
    [Mes]        INT        NULL,
    [ayo]        INT        NULL,
    [Emitio]     INT        NULL,
    CONSTRAINT [PK_Precio] PRIMARY KEY CLUSTERED ([PrecioId] ASC),
    CONSTRAINT [FK_Precio_Producto] FOREIGN KEY ([ProductoId]) REFERENCES [cat].[Producto] ([ProductoId])
);



