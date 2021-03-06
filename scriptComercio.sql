USE [master]
GO
/****** Object:  Database [db_Comercio]    Script Date: 10/03/2019 23:45:12 ******/
CREATE DATABASE [db_Comercio]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_Comercio', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\db_Comercio.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db_Comercio_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\db_Comercio_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [db_Comercio] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_Comercio].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_Comercio] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_Comercio] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_Comercio] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_Comercio] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_Comercio] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_Comercio] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db_Comercio] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_Comercio] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_Comercio] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_Comercio] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_Comercio] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_Comercio] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_Comercio] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_Comercio] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_Comercio] SET  DISABLE_BROKER 
GO
ALTER DATABASE [db_Comercio] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_Comercio] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_Comercio] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_Comercio] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_Comercio] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_Comercio] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_Comercio] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_Comercio] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [db_Comercio] SET  MULTI_USER 
GO
ALTER DATABASE [db_Comercio] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_Comercio] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_Comercio] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_Comercio] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_Comercio] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db_Comercio] SET QUERY_STORE = OFF
GO
USE [db_Comercio]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 10/03/2019 23:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Endereco]    Script Date: 10/03/2019 23:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Endereco](
	[IdEndereco] [int] IDENTITY(1,1) NOT NULL,
	[IdPessoa] [int] NOT NULL,
	[Logradouro] [varchar](60) NOT NULL,
	[Bairro] [varchar](15) NOT NULL,
	[Cidade] [varchar](20) NOT NULL,
	[Estado] [varchar](2) NOT NULL,
	[Cep] [varchar](8) NOT NULL,
 CONSTRAINT [PK_Endereco] PRIMARY KEY CLUSTERED 
(
	[IdEndereco] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 10/03/2019 23:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[IdItem] [int] IDENTITY(1,1) NOT NULL,
	[IdProduto] [int] NOT NULL,
	[IdPedido] [int] NOT NULL,
	[Quantidade] [int] NOT NULL,
	[Subtotal] [decimal](13, 2) NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[IdItem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 10/03/2019 23:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[IdPedido] [int] IDENTITY(1,1) NOT NULL,
	[IdPessoa] [int] NULL,
	[DataPedido] [datetime] NOT NULL,
	[ValorTotal] [decimal](13, 2) NOT NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[IdPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 10/03/2019 23:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoa](
	[IdPessoa] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[TipoPessoa] [varchar](1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Cpf] [varchar](14) NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[NumeroCelular] [varchar](14) NULL,
	[Foto] [varbinary](max) NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[IdPessoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produto]    Script Date: 10/03/2019 23:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto](
	[IdProduto] [int] IDENTITY(1,1) NOT NULL,
	[IdCategoria] [int] NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[Preco] [decimal](13, 2) NOT NULL,
	[DiretorioImagem] [varchar](100) NULL,
 CONSTRAINT [PK_Produto] PRIMARY KEY CLUSTERED 
(
	[IdProduto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 10/03/2019 23:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](30) NOT NULL,
	[SenhaHash] [varbinary](64) NOT NULL,
	[SenhaSalt] [varbinary](128) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Endereco]  WITH CHECK ADD  CONSTRAINT [FK_Endereco_Cliente] FOREIGN KEY([IdPessoa])
REFERENCES [dbo].[Pessoa] ([IdPessoa])
GO
ALTER TABLE [dbo].[Endereco] CHECK CONSTRAINT [FK_Endereco_Cliente]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Pedido] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedido] ([IdPedido])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Pedido]
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Produto] FOREIGN KEY([IdProduto])
REFERENCES [dbo].[Produto] ([IdProduto])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Produto]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Pessoa] FOREIGN KEY([IdPessoa])
REFERENCES [dbo].[Pessoa] ([IdPessoa])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Pessoa]
GO
ALTER TABLE [dbo].[Pessoa]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Pessoa] CHECK CONSTRAINT [FK_Cliente_Usuario]
GO
ALTER TABLE [dbo].[Produto]  WITH CHECK ADD  CONSTRAINT [FK_Produto_Categoria] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[Produto] CHECK CONSTRAINT [FK_Produto_Categoria]
GO
USE [master]
GO
ALTER DATABASE [db_Comercio] SET  READ_WRITE 
GO
