CREATE TABLE [dbo].[PerfilDireccionFiscal] (
    [UsuarioId]            NVARCHAR (128) NOT NULL,
    [RFC]                  NVARCHAR (13)  NOT NULL,
    [CodigoPostal]         NVARCHAR (5)   NOT NULL,
    [Calle]                NVARCHAR (150) NOT NULL,
    [NumeroExterior]       NVARCHAR (50)  NOT NULL,
    [NumeroInterior]       NVARCHAR (50)  NULL,
    [Colonia]              NVARCHAR (150) NOT NULL,
    [FechaModificacion]    DATETIME       NOT NULL,
    [UsuarioModificadorId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_PerfilDireccionFiscal] PRIMARY KEY CLUSTERED ([UsuarioId] ASC),
    CONSTRAINT [FK_PerfilDireccionFiscal_Perfil] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Perfil] ([UsuarioId])
);

