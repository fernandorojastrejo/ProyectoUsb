CREATE TABLE [dbo].[Perfil] (
    [UsuarioId]     NVARCHAR (128) NOT NULL,
    [Nombre]        NVARCHAR (50)  NOT NULL,
    [APaterno]      NVARCHAR (50)  NOT NULL,
    [AMaterno]      NVARCHAR (50)  NOT NULL,
    [Activo]        BIT            NOT NULL,
    [FechaRegistro] DATETIME       NOT NULL,
    CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED ([UsuarioId] ASC),
    CONSTRAINT [FK_Perfil_AspNetUsers] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

