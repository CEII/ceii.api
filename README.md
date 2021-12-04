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

### Migraciones - Crear
Desde el directorio que contiene el contexto de la base de datos (Ceii.Api.Data)

**Plantilla**

`dotnet ef --startup-project <ruta-de-pj-de-inicio> migrations add <nombre-migracion>`

**Ejemplo**

`dotnet ef --startup-project ../Ceii.Api.Core migrations add InitialCreate`

### Migraciones - Actualizar BD
Para actualizar la base de datos (ejecutar migraciones pendientes), desde la carpeta que contiene Ceii.Api.Data:

**Plantilla**

`dotnet ef --startup-project <ruta-de-pj-de-inicio> database update`

**Ejemplo**

`dotnet ef --startup-project ../Ceii.Api.Core database update`
---
## Flujo de trabajo

### Flujo
1. Crear una rama segun convenciones de **git flow**.

    **OJO**: Solo usar git flow para la creacion de ramas

2. Commitear segun [estandares](#estandar-para-repositorios)

3. Crear un Pull Request desde **gh cli**

4. Avisar al reviewer de la creacion del PR
    - Si son necesarios cambios posterior a la revision, y han realizado nuevos commits unicamente pushearlos a la rama correspondiente


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
    
    Permite crear PR's a traves de flags.
    - `-f` completa la descripcion con los nombres de los commits de la rama existente
    - `-a <login>` completa el assignee por su usuario. Usar @me para autoasignarse
    - `-r <login>` completa el reviewer por su usuario.

    Ejemplo (desde rama ejemplo/PR-01):
    
    **`gh pr create -f -a @me -r wmoralesdev`**

---

## Estandar para repositorios

### Ramas
- Los nombres de las ramas tendran el prefijo de git flow y de nombre el numero de ticket en el proyecto seguido de una breve descripcion 

    `(feature|bug|hotfix)/GH-PJ-NUM <descripcion-breve>`


### Commits
- Practicar mensajes de commmit semanticos. Los mensajes seguirán un formato estandar:

`(feat|fix|docs|style|refactor|perf|test|chore): <descripcion-breve>`

#### Tipos de commit
- **`feat`**: Agregar una característica
- **`fix`**: Solucionar un bug
- **`docs`**: Cambios en la documentación
- **`style`**: Cambio en el formato del código (salto de linea, indentacion, etc. que no implique cambio en funcionalidad)
- **`refactor`**: Refactorizar código
- **`perf`**: Actualizar código en su desempeño
- **`test`**: Agregar tests, actualizar o refactorizar tests
- **`chore`**: Actualizar sin afectar el uso. Por ejemplo, actualización de paquetes, cambio de mensajes en strings, etc.
