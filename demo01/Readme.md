# SQL Connect 2019 - Demo 01
## .NET Core - API Rest con conexión a Base de datos SQL Server

## **Requisitos**  
* Contenedor de la Base de Datos "hr-database:4.0" proporcionado por [Carlos Robles](https://github.com/dbamaster)
    
    docker run --name demo_01 --network=net_demo_sql_server --hostname demo_01 --publish 1433:1433 --detach crobles10/hr-database:4.0

## Librerías instaladas en el proyecto.

	dotnet add package Microsoft.EntityFrameworkCore.SqlServer
	dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
	dotnet add package Microsoft.EntityFrameworkCore.Design
	dotnet tool install --global dotnet-aspnet-codegenerator

## Comandos de asp codegenerator para crear controladores basados en modelos para APIs.
	
	dotnet aspnet-codegenerator controller -name RegionesController -actions -api -m Region -dc Contexto -outDir Controllers
