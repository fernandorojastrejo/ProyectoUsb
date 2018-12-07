CREATE TABLE [dbo].[Banner] (
    [BannerId]    INT            IDENTITY (1, 1) NOT NULL,
    [CategoriaId] INT            NOT NULL,
    [UrlImagen]   NVARCHAR (500) NOT NULL,
    [orden]       INT            NULL,
    CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED ([BannerId] ASC),
    CONSTRAINT [FK_Banner_Categoria] FOREIGN KEY ([CategoriaId]) REFERENCES [cat].[Categoria] ([CategoriaId])
);





