CREATE TABLE [cat].[Categoria] (
    [CategoriaId]  INT           IDENTITY (1, 1) NOT NULL,
    [Descripcion]  NVARCHAR (50) NOT NULL,
    [TiempoBanner] INT           NULL,
    CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED ([CategoriaId] ASC)
);



