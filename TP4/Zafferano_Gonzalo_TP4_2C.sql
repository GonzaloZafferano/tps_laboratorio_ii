USE [master]
GO
/****** Object:  Database [ZAFFERANO_GONZALO_TP4_2C]    Script Date: 16/06/2022 17:09:20 ******/
CREATE DATABASE [ZAFFERANO_GONZALO_TP4_2C]
 CONTAINMENT = NONE
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ZAFFERANO_GONZALO_TP4_2C].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET ARITHABORT OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET RECOVERY FULL 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET  MULTI_USER 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ZAFFERANO_GONZALO_TP4_2C', N'ON'
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET QUERY_STORE = OFF
GO
USE [ZAFFERANO_GONZALO_TP4_2C]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 16/06/2022 17:09:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Dni] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
	[Activo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 16/06/2022 17:09:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[Id_Compra] [int] IDENTITY(1,1) NOT NULL,
	[Dni_Cliente] [int] NOT NULL,
	[Descuento] [float] NOT NULL,
	[Precio_Final_Acumulado_Con_Descuento] [float] NOT NULL,
	[Id_Usuario] [int] NOT NULL,
	[Medio_De_Pago] [varchar](50) NOT NULL,
	[Fecha_Compra] [date] NOT NULL,
	[Detalles_Compra] [text] NOT NULL,
 CONSTRAINT [PK_Compras] PRIMARY KEY CLUSTERED 
(
	[Id_Compra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 16/06/2022 17:09:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Dni] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Puesto] [varchar](50) NOT NULL,
	[Salario] [float] NOT NULL,
	[NombreUsuario] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Activo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 16/06/2022 17:09:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[Id_Producto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Producto] [varchar](50) NOT NULL,
	[Descripcion] [text] NOT NULL,
	[Precio] [float] NOT NULL,
	[Tamanio] [varchar](50) NULL,
	[Tiene_Color] [bit] NULL,
	[Categoria] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[Id_Producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Clientes] ([Dni], [Nombre], [Apellido], [Telefono], [Activo]) VALUES (16987656, N'Roberto', N'Carlos', N'11-2222-3333', N'Activo')
INSERT [dbo].[Clientes] ([Dni], [Nombre], [Apellido], [Telefono], [Activo]) VALUES (21659989, N'Juan', N'Martin', N'11-9876-7854', N'Activo')
INSERT [dbo].[Clientes] ([Dni], [Nombre], [Apellido], [Telefono], [Activo]) VALUES (33259797, N'Daiana', N'Villar', N'15-1236-1198', N'Activo')
INSERT [dbo].[Clientes] ([Dni], [Nombre], [Apellido], [Telefono], [Activo]) VALUES (34659797, N'Lucia', N'Arruti', N'15-1234-9876', N'Activo')
INSERT [dbo].[Clientes] ([Dni], [Nombre], [Apellido], [Telefono], [Activo]) VALUES (35498796, N'Gonzalo', N'Stregaro', N'15-3215-9876', N'Activo')
GO
SET IDENTITY_INSERT [dbo].[Compras] ON 

INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (1, 1000000, 0, 2600, 1, N'1', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Logos. Id Producto: 2
Nombre Producto: Logos
Descripcion: Logos personalizados.
Precio Base: $1.300,00
Tamaño: Chico
Detalles adicionales: -
($1.300,00 x 1 unidades = $1.300,00)
2) Logos. Id Producto: 2
Nombre Producto: Logos
Descripcion: Logos personalizados.
Precio Base: $1.300,00
Tamaño: Chico
Detalles adicionales: -
($1.300,00 x 1 unidades = $1.300,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (2, 16987656, 0, 3900, 1, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Logos. Id Producto: 2
Nombre Producto: Logos
Descripcion: Logos personalizados.
Precio Base: $1.300,00
Tamaño: Chico
Detalles adicionales: -
($1.300,00 x 1 unidades = $1.300,00)
2) Logos. Id Producto: 2
Nombre Producto: Logos
Descripcion: Logos personalizados.
Precio Base: $1.300,00
Tamaño: Chico
Detalles adicionales: -
($1.300,00 x 1 unidades = $1.300,00)
3) Logos. Id Producto: 2
Nombre Producto: Logos
Descripcion: Logos personalizados.
Precio Base: $1.300,00
Tamaño: Chico
Detalles adicionales: -
($1.300,00 x 1 unidades = $1.300,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (3, 21659989, 0, 1315, 2, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Logos. Id Producto: 2
Nombre Producto: Logos
Descripcion: Logos personalizados.
Precio Base: $1.300,00
Tamaño: Chico
Detalles adicionales: -
($1.300,00 x 1 unidades = $1.300,00)
2) Hola A4. Id Producto: 4
Nombre Producto: Hola A4
Descripcion: Impresiones en Hoja A4.
Precio Base: $05,00
Color: NO
Detalles adicionales: -
($05,00 x 1 unidades = $05,00)
3) Folletos. Id Producto: 1
Nombre Producto: Folletos
Descripcion: Impresion de folletos.
Precio Base: $10,00
Color: NO
Detalles adicionales: -
($10,00 x 1 unidades = $10,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (4, 1000000, 0, 2519, 2, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Hoja Oficio. Id Producto: 5
Nombre Producto: Hoja Oficio
Descripcion: Impresiones en hoja oficio.
Precio Base: $07,00
Color: NO
Detalles adicionales: -
($07,00 x 1 unidades = $07,00)
2) Hoja Oficio. Id Producto: 5
Nombre Producto: Hoja Oficio
Descripcion: Impresiones en hoja oficio.
Precio Base: $07,00
Color: NO
Detalles adicionales: -
($07,00 x 1 unidades = $07,00)
3) Banners. Id Producto: 6
Nombre Producto: Banners
Descripcion: Diseño de banners personalizados.
Precio Base: $2.500,00
Tamaño: Chico
Detalles adicionales: -
($2.500,00 x 1 unidades = $2.500,00)
4) Hola A4. Id Producto: 4
Nombre Producto: Hola A4
Descripcion: Impresiones en Hoja A4.
Precio Base: $05,00
Color: NO
Detalles adicionales: -
($05,00 x 1 unidades = $05,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (5, 1000000, 0, 55, 3, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Hola A4. Id Producto: 4
Nombre Producto: Hola A4
Descripcion: Impresiones en Hoja A4.
Precio Base: $05,00
Color: NO
Detalles adicionales: -
($05,00 x 1 unidades = $05,00)
2) Hola A4. Id Producto: 4
Nombre Producto: Hola A4
Descripcion: Impresiones en Hoja A4.
Precio Base: $10,00
Color: SI
Detalles adicionales: -
($10,00 x 5 unidades = $50,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (6, 33259797, 0, 11100, 3, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Cartel. Id Producto: 7
Nombre Producto: Cartel
Descripcion: Diseño de cartel personalizado.
Precio Base: $11.100,00
Tamaño: Grande
Detalles adicionales: -
($11.100,00 x 1 unidades = $11.100,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (7, 34659797, 0, 28, 3, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Hoja Oficio. Id Producto: 5
Nombre Producto: Hoja Oficio
Descripcion: Impresiones en hoja oficio.
Precio Base: $07,00
Color: NO
Detalles adicionales: -
($07,00 x 1 unidades = $07,00)
2) Hoja Oficio. Id Producto: 5
Nombre Producto: Hoja Oficio
Descripcion: Impresiones en hoja oficio.
Precio Base: $07,00
Color: NO
Detalles adicionales: -
($07,00 x 1 unidades = $07,00)
3) Hoja Oficio. Id Producto: 5
Nombre Producto: Hoja Oficio
Descripcion: Impresiones en hoja oficio.
Precio Base: $07,00
Color: NO
Detalles adicionales: -
($07,00 x 1 unidades = $07,00)
4) Hoja Oficio. Id Producto: 5
Nombre Producto: Hoja Oficio
Descripcion: Impresiones en hoja oficio.
Precio Base: $07,00
Color: NO
Detalles adicionales: -
($07,00 x 1 unidades = $07,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (8, 1000000, 0, 30, 4, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Hola A4. Id Producto: 4
Nombre Producto: Hola A4
Descripcion: Impresiones en Hoja A4.
Precio Base: $05,00
Color: NO
Detalles adicionales: -
($05,00 x 1 unidades = $05,00)
2) Hola A4. Id Producto: 4
Nombre Producto: Hola A4
Descripcion: Impresiones en Hoja A4.
Precio Base: $05,00
Color: NO
Detalles adicionales: -
($05,00 x 1 unidades = $05,00)
3) Hola A4. Id Producto: 4
Nombre Producto: Hola A4
Descripcion: Impresiones en Hoja A4.
Precio Base: $05,00
Color: NO
Detalles adicionales: -
($05,00 x 1 unidades = $05,00)
4) Hola A4. Id Producto: 4
Nombre Producto: Hola A4
Descripcion: Impresiones en Hoja A4.
Precio Base: $05,00
Color: NO
Detalles adicionales: -
($05,00 x 1 unidades = $05,00)
5) Hola A4. Id Producto: 4
Nombre Producto: Hola A4
Descripcion: Impresiones en Hoja A4.
Precio Base: $05,00
Color: NO
Detalles adicionales: -
($05,00 x 1 unidades = $05,00)
6) Hola A4. Id Producto: 4
Nombre Producto: Hola A4
Descripcion: Impresiones en Hoja A4.
Precio Base: $05,00
Color: NO
Detalles adicionales: -
($05,00 x 1 unidades = $05,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (9, 35498796, 0, 12400, 4, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Cartel. Id Producto: 7
Nombre Producto: Cartel
Descripcion: Diseño de cartel personalizado.
Precio Base: $3.700,00
Tamaño: Chico
Detalles adicionales: -
($3.700,00 x 1 unidades = $3.700,00)
2) Cartel. Id Producto: 7
Nombre Producto: Cartel
Descripcion: Diseño de cartel personalizado.
Precio Base: $3.700,00
Tamaño: Chico
Detalles adicionales: -
($3.700,00 x 1 unidades = $3.700,00)
3) Banners. Id Producto: 6
Nombre Producto: Banners
Descripcion: Diseño de banners personalizados.
Precio Base: $2.500,00
Tamaño: Chico
Detalles adicionales: -
($2.500,00 x 1 unidades = $2.500,00)
4) Banners. Id Producto: 6
Nombre Producto: Banners
Descripcion: Diseño de banners personalizados.
Precio Base: $2.500,00
Tamaño: Chico
Detalles adicionales: -
($2.500,00 x 1 unidades = $2.500,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (10, 1000000, 0, 14, 5, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Hoja Oficio. Id Producto: 5
Nombre Producto: Hoja Oficio
Descripcion: Impresiones en hoja oficio.
Precio Base: $07,00
Color: NO
Detalles adicionales: -
($07,00 x 1 unidades = $07,00)
2) Hoja Oficio. Id Producto: 5
Nombre Producto: Hoja Oficio
Descripcion: Impresiones en hoja oficio.
Precio Base: $07,00
Color: NO
Detalles adicionales: -
($07,00 x 1 unidades = $07,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (11, 34659797, 0, 3700, 5, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Cartel. Id Producto: 7
Nombre Producto: Cartel
Descripcion: Diseño de cartel personalizado.
Precio Base: $3.700,00
Tamaño: Chico
Detalles adicionales: Colores vibrantes en los bordes.
($3.700,00 x 1 unidades = $3.700,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (12, 35498796, 0, 11100, 6, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Cartel. Id Producto: 7
Nombre Producto: Cartel
Descripcion: Diseño de cartel personalizado.
Precio Base: $11.100,00
Tamaño: Grande
Detalles adicionales: Mas color en los bordes.
($11.100,00 x 1 unidades = $11.100,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (13, 35498796, 0, 4900, 1, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Logos. Id Producto: 2
Nombre Producto: Logos
Descripcion: Logos personalizados.
Precio Base: $3.900,00
Tamaño: Grande
Detalles adicionales: Un diseño para un local de comida.
($3.900,00 x 1 unidades = $3.900,00)
2) Tarjetas. Id Producto: 3
Nombre Producto: Tarjetas
Descripcion: Tarjetas personalizadas.
Precio Base: $10,00
Color: NO
Detalles adicionales: Tarjetas de presentacion.
($10,00 x 100 unidades = $1.000,00)
')
INSERT [dbo].[Compras] ([Id_Compra], [Dni_Cliente], [Descuento], [Precio_Final_Acumulado_Con_Descuento], [Id_Usuario], [Medio_De_Pago], [Fecha_Compra], [Detalles_Compra]) VALUES (14, 21659989, 0, 40, 1, N'0', CAST(N'2022-06-16' AS Date), N'<----Productos en carrito---->
1) Tarjetas. Id Producto: 3
Nombre Producto: Tarjetas
Descripcion: Tarjetas personalizadas.
Precio Base: $10,00
Color: NO
Detalles adicionales: -
($10,00 x 4 unidades = $40,00)
')
SET IDENTITY_INSERT [dbo].[Compras] OFF
GO
SET IDENTITY_INSERT [dbo].[Empleados] ON 

INSERT [dbo].[Empleados] ([Id], [Dni], [Nombre], [Apellido], [Puesto], [Salario], [NombreUsuario], [Password], [Activo]) VALUES (1, 12345678, N'Fabian', N'Guetta', N'Jefe', 55000, N'Fguetta', N'aaaa', N'Activo')
INSERT [dbo].[Empleados] ([Id], [Dni], [Nombre], [Apellido], [Puesto], [Salario], [NombreUsuario], [Password], [Activo]) VALUES (2, 33214567, N'Veronica', N'Gomez', N'Administrador', 49000, N'Vero90', N'bbbb', N'Activo')
INSERT [dbo].[Empleados] ([Id], [Dni], [Nombre], [Apellido], [Puesto], [Salario], [NombreUsuario], [Password], [Activo]) VALUES (3, 29789464, N'Lucas', N'Ramos', N'Empleado', 33000, N'Lramos', N'cccc', N'Activo')
INSERT [dbo].[Empleados] ([Id], [Dni], [Nombre], [Apellido], [Puesto], [Salario], [NombreUsuario], [Password], [Activo]) VALUES (4, 35229019, N'Melina', N'Villar', N'Administrador', 49500, N'Melina92', N'dddd', N'Activo')
INSERT [dbo].[Empleados] ([Id], [Dni], [Nombre], [Apellido], [Puesto], [Salario], [NombreUsuario], [Password], [Activo]) VALUES (5, 30654987, N'Karen', N'Zalas', N'Empleado', 33950, N'Karenz', N'eeee', N'Activo')
INSERT [dbo].[Empleados] ([Id], [Dni], [Nombre], [Apellido], [Puesto], [Salario], [NombreUsuario], [Password], [Activo]) VALUES (6, 33215978, N'Carolina', N'Guttierrez', N'Empleado', 35500, N'Caro92', N'ffff', N'Activo')
SET IDENTITY_INSERT [dbo].[Empleados] OFF
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 

INSERT [dbo].[Productos] ([Id_Producto], [Nombre_Producto], [Descripcion], [Precio], [Tamanio], [Tiene_Color], [Categoria]) VALUES (1, N'Folletos', N'Impresion de folletos.', 10, NULL, 0, N'Impresion')
INSERT [dbo].[Productos] ([Id_Producto], [Nombre_Producto], [Descripcion], [Precio], [Tamanio], [Tiene_Color], [Categoria]) VALUES (2, N'Logos', N'Logos personalizados.', 1300, N'1', NULL, N'Disenio')
INSERT [dbo].[Productos] ([Id_Producto], [Nombre_Producto], [Descripcion], [Precio], [Tamanio], [Tiene_Color], [Categoria]) VALUES (3, N'Tarjetas', N'Tarjetas personalizadas.', 10, NULL, 0, N'Impresion')
INSERT [dbo].[Productos] ([Id_Producto], [Nombre_Producto], [Descripcion], [Precio], [Tamanio], [Tiene_Color], [Categoria]) VALUES (4, N'Hola A4', N'Impresiones en Hoja A4.', 5, NULL, 0, N'Impresion')
INSERT [dbo].[Productos] ([Id_Producto], [Nombre_Producto], [Descripcion], [Precio], [Tamanio], [Tiene_Color], [Categoria]) VALUES (5, N'Hoja Oficio', N'Impresiones en hoja oficio.', 7, NULL, 0, N'Impresion')
INSERT [dbo].[Productos] ([Id_Producto], [Nombre_Producto], [Descripcion], [Precio], [Tamanio], [Tiene_Color], [Categoria]) VALUES (6, N'Banners', N'Diseño de banners personalizados.', 2500, N'1', NULL, N'Disenio')
INSERT [dbo].[Productos] ([Id_Producto], [Nombre_Producto], [Descripcion], [Precio], [Tamanio], [Tiene_Color], [Categoria]) VALUES (7, N'Cartel', N'Diseño de cartel personalizado.', 3700, N'1', NULL, N'Disenio')
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
USE [master]
GO
ALTER DATABASE [ZAFFERANO_GONZALO_TP4_2C] SET  READ_WRITE 
GO
