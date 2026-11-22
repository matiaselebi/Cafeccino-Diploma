# Cafeccino - Trabajo de Diploma

Sistema de gestión para una cafetería, desarrollado como Trabajo de Diploma de Ingeniería en Sistemas (Universidad Abierta Interamericana).
Registra las ventas en el local con su factura y administra las compras de insumos, desde la sugerencia de compra hasta la recepción de la mercadería.

**Alumno:** Matias Elebi  
**Versión:** 3.0

## Credenciales de prueba

| Usuario | Contraseña | Perfil |
|---|---|---|
| mati | 12 | administrador |

## Funcionalidades

### Negocio
- **Ventas:** nueva venta con carrito, búsqueda o alta del cliente, pago en efectivo o con tarjeta (con recargo), factura en PDF y consulta de facturas.
- **Compras de insumos:** insumos y recetas, sugerencia de compra según el stock, solicitudes de cotización a proveedores, comparación de cotizaciones, órdenes de compra en PDF y recepción de insumos.
- **Maestros:** gestión de productos y bitácora de cambios de productos.

### Técnicas
- Inicio y cierre de sesión, cambio de contraseña y bloqueo de usuarios por intentos fallidos.
- Encriptación irreversible (hash de contraseñas) y reversible (datos sensibles).
- Perfiles y familias de permisos con el patrón **Composite**: cada usuario ve solo el menú que su perfil permite.
- Multi-idioma (español e inglés) con el patrón **Observer**.
- Bitácora de eventos con filtros por fecha, módulo, evento y criticidad.
- Bitácora de cambios de productos: un trigger guarda cada versión del producto en `Producto_C`, con filtros por código, nombre y fechas, y permite activar una versión anterior.
- Dígitos verificadores horizontales y verticales (DVH y DVV) para controlar la integridad de los datos.
- Backup y restore de la base de datos.

### Adicionales
- **Reporte inteligente:** 14 reportes de ventas, clientes, compras y proveedores, con predicciones de stock, rentabilidad y temporadas, y exportación a PDF.
- **Serialización de clientes** a XML y des-serialización.
- **Ayuda en línea:** menú Ayuda con un PDF por pantalla, filtrado por los permisos del usuario.

## Tecnologías y arquitectura

- C# WinForms, .NET Framework 4.8.
- SQL Server.
- Arquitectura en capas:
  - **UI:** pantallas.
  - **BLL:** reglas de negocio.
  - **DAL:** acceso a datos.
  - **BE:** entidades.
  - **SERVICIOS:** encriptación, sesión (Singleton), idiomas (Observer), dígitos verificadores y serialización.
- Facturas, órdenes de compra y reportes en PDF con la impresora "Microsoft Print to PDF" de Windows. No se usan librerías externas.

## Requisitos

- Windows 10 u 11 con .NET Framework 4.8.
- SQL Server 2019 o superior (sirve la versión Express).
- El usuario de Windows tiene que poder crear bases de datos en SQL Server (rol `dbcreator`).

## Instalación

### Con el instalador
1. Ejecutar `Cafeccino_2.0.msi`. No pide permisos de administrador: se instala para el usuario actual.
2. Elegir el servidor de SQL Server. El instalador crea la base `Cafeccino` con los datos iniciales (idiomas, permisos y el usuario administrador).
3. Abrir Cafeccino e iniciar sesión con las credenciales de arriba.

Si ya hay una versión anterior instalada, el instalador la reemplaza. Al abrir el programa, la base se actualiza sola con lo que le falte de la versión 2.0.

### Desde el código
1. Si se descargó el proyecto como .zip, desbloquear los archivos desde PowerShell, en la carpeta del proyecto. Sin esto, Visual Studio no compila los formularios:
   ```powershell
   Get-ChildItem -Recurse | Unblock-File
   ```
2. Crear la base ejecutando `Cafeccino.sql` en SQL Server Management Studio.
3. Revisar la cadena de conexión en `UI/App.config`. Por defecto usa `Data Source=localhost;Initial Catalog=Cafeccino;Integrated Security=True`.
4. Abrir `CAFECCINO.sln` en Visual Studio, compilar y ejecutar el proyecto `UI`.

## Estructura

```
CAFECCINO.sln
├── UI/          Formularios, comprobantes PDF y ayuda en línea (UI/Ayuda, PDF incluidos en el .exe)
├── BLL/         Lógica de negocio
├── DAL/         Acceso a datos (incluye el script de actualización a la 2.0)
├── BE/          Entidades
├── SERVICIOS/   Servicios técnicos (Singleton, Observer, encriptación, dígitos verificadores, serialización)
└── Cafeccino.sql   Script de la base de datos
```
