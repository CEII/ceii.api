# CEII API
Desarrollo en .NET 6.0

## Resumen
- [Configuraciones](#configuracion)
- [Proyectos](#proyectos)
- [Paquetes](#paquetes)
- [Herramientas](#herramientas)
- [Comandos](#comandos)
- [Flujo de trabajo](#flujo-de-trabajo)

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

4. Correr proyecto localmente, desde raiz del repositorio. Se pueden seleccionar distintos perfiles, se recomienda usar ceii_api

    ```bash
    dotnet watch run --project src/Ceii.Api.Core --launch-profile ceii_api
    ```

- De preferencia, crear alias para los comandos

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


---
## Flujo de trabajo

### Flujo
1. Crear una rama segun convenciones de **git flow**.

    **OJO**: Solo usar git flow para la creacion de ramas

2. Commitear segun [estandares](#estandar-para-commits)

3. Crear un Pull Request desde **gh cli**

### Git Flow
Para facilitar el desarrollo, se usará [GitFlow](https://danielkummer.github.io/git-flow-cheatsheet/) y su manejo por ramas.

#### Prefijos de ramas
- Feature: feature/
- Bugfix: bugfix/
- Release: release/
- Hotfix: hotfix/

#### Escenarios
- Feature: para agregar funcionalidad
- Bugfix: para reparar errores que no están en producción
- Release: para publicar una versión a producción
- Hotfix: para reparar errores en caliente, generados o encontrados en producción

### GH CLI
Para facilitar la creacion de PR's. [Instalar](https://cli.github.com/)

#### Comandos

- **`gh pr create`**
    
    Permite crear ramas a traves de flags.
    - `-f` completa la descripcion con los nombres de los commits de la rama existente
    - `-a <login>` completa el assignee por su usuario. Usar @me para autoasignarse
    - `-r <login>` completa el reviewer por su usuario.

    Ejemplo (desde rama ejemplo/PR-01):
    
    **`gh pr create -f -a @me -r wmoralesdev -p 'CEII Portal'`**