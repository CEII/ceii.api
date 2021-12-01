# CEII API
Desarrollo en .NET 6.0

## Resumen
- [Configuraciones](#configuracion)
- [Proyectos](#proyectos)
- [Paquetes](#paquetes)
- [Herramientas](#herramientas)
- [Comandos](#comandos)


---
## Configuracion

### Prerequisitos
- WSL 2
- [net6.0 SDK y Runtime](https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#2110-)
- [Docker](https://docs.docker.com/desktop/windows/wsl/)
- [**Opcional**: Sql Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)

### Pasos
1. Montar contenedores con `docker-compose up -d`

2. Pegar en `src/Ceii.Api.Core` archivo `appsettings.Development.json`

3. Al configurar la primera vez, o cuando exista una nueva migración actualizar base de datos. [Ver mas (migraciones)](#comandos)

4. Correr proyecto localmente, desde raiz del repositorio 

    ```bash
    dotnet watch run --project src/Ceii.Api.Core
    ```

---
## Proyectos

### Ceii.Api.Core
Api en general, manejo de enpoints y controladores

### Ceii.Api.Data
Contenido de la información (entidades, migraciones)


### Ceii.Api.Services
Servicios provistos para Core

---
## Paquetes
- EntityFrameworkCore (ORM)
- EntityFrameworkCore.SqlServer (Acceso a la bd)
- Swashbuckle.AspNetCore.Swagger (Documentacion de EP)


---
## Herramientas
### EF Core CLI
1. Instalar herramienta

    `dotnet tool install --global dotnet-ef`


2. Agregar al RC (bashrc o zshrc)

    Para bash (terminal por defecto de WSL 2)
    ```
    code ~/.bashrc
    ```

    Para zshrc (si se tiene instalado ZSH)
    ```
    code ~/.zshrc
    ```

    Agregar al final del archivo abierto

    ```bash
    # Agregar al final
    export PATH="$PATH:$HOME/.dotnet/tools/"
    ```


---
## Comandos

### Migraciones
Desde el directorio que contiene el contexto de la base de datos (Ceii.Api.Data)

**Plantilla**

`dotnet ef --startup-project <ruta-de-pj-de-inicio> migrations add <nombre-migracion>`

**Ejemplo**

`dotnet ef --startup-project ../Ceii.Api.Api migrations add InitialCreate`