CREATE TABLE [cat].[Producto] (
    [ProductoId]      INT            IDENTITY (1, 1) NOT NULL,
    [CategoriaId]     INT            NOT NULL,
    [Codigo]          NVARCHAR (128) NOT NULL,
    [CodigoBarra]     NVARCHAR (128) NULL,
    [Nombre]          NVARCHAR (150) NOT NULL,
    [Pieza]           INT            NULL,
    [Descripcion]     NVARCHAR (500) NOT NULL,
    [Color]           NVARCHAR (128) NULL,
    [Capacidad]       NVARCHAR (128) NULL,
    [Material]        NVARCHAR (250) NULL,
    [Activo]          BIT            NULL,
    [Medida]          NVARCHAR (128) NULL,
    [EsPrincipal]     BIT            CONSTRAINT [DF_Producto_EsPrincipal] DEFAULT ((0)) NULL,
    [CodigoPrincipal] NVARCHAR (128) NULL,
    CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED ([ProductoId] ASC),
    CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY ([CategoriaId]) REFERENCES [cat].[Categoria] ([CategoriaId])
);















