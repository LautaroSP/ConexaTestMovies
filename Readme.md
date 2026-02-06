## Conexa.TestMovies

API Rest en .NET 10 para gestion de películas.
Ejercicio Práctico: Aplicación de Gestión de Películas

## Get Started
Guia para ejecutar el proyecto localmente.

### Requisitos Previos
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio Insiders](https://visualstudio.microsoft.com/vs/) o cualquier IDE compatible con .NET 10.

### Pasos para Ejecutar el Proyecto
1. Clona el repositorio:
   https://github.com/LautaroSP/ConexaTestMovies
2. Abrir la solución en Visual Studio. Se encuentra en la carpeta (Conexa.TestMovies)
3. Verificar el proyecto de inicio. Debe ser Conexa.TestMovies
   - En caso de que no sea el proyecto de inicio hacer doble click sobre Conexa.TestMovies(webApi)
   - Buscar Establecer proyecto de inicio y hacer click.
4. Compilar solucion.
   4.a - En caso de no compilar, restaurar paquetes NuGet.
5. Ejecutar las migraciones para crear la bdd con sus entidades:
   - Abrir la consola del administrador de paquetes en Visual Studio.
   - Seleccionar el proyecto 'Conexa.TestMovies.Persistance' como proyecto predeterminado.
   - Ejecutar el comando:
     ```
     Update-Database
     ```
6. Ejecutar la aplicación en https
7. Abrir el navegador y navegar a:
   https://localhost:7191/swagger/index.html para acceder a la documentación de la API.

### Librerias Utilizadas
- Entity Framework Core: Para el acceso a datos y mapeo objeto-relacional.
- Swashbuckle.AspNetCore: Para la generación de documentación Swagger.
- MediatR: Para la implementación del patrón CQRS.
- FluentValidation: Para la validación de datos de entrada.
- Refit: Para la serialización y deserialización JSON.
- Moq: Para la creación de mocks en pruebas unitarias.
- Shouldly: Para aserciones en pruebas unitarias.
- nUnit: Framework de pruebas unitarias.
- Sqlite: Base de datos ligera para desarrollo y pruebas.

### Estructura del Proyecto
Utilice la arquitectura DDD para organizar el código en capas:
- Domain: Contiene las entidades, agregados y lógica de negocio.
- Application: Contiene casos de uso, comandos y consultas.
- Persistance: Contiene la configuración de la base de datos y los repositorios.

Luego para acceder a una Api Externa, utilice la capa de API Clients.
- API Clients: Contiene clientes para consumir APIs externas.

Y luego esta la capa de Tests para los test unitarios.

Utilice tambien el patron Result llamado BaseResponse para estandarizar las respuestas de la API,
incluyendo información sobre el éxito o fracaso de la operación, mensajes de error y datos resultantes.

Los repositories heredan una interfaz generica IAsyncRepository, 
que define métodos comunes para operaciones CRUD, y luego cada entidad tiene su propio repositorio específico que implementa esta interfaz.

### Endpoints:
User:
- POST `/api/v1/user/register` — Registrar usuario. (201)
- POST `/api/v1/user/login` — Login, devuelve `UserVM` con token JWT. (200)

Movies:
- GET `/starwars` — Obtiene lista desde https://www.swapi.tech/ (sin autenticación).
- POST `/syncMovies` — Sincroniza BDD con API externa. (AdminOnly)
- POST `/api/v1/movies/create` — Crear película. (AdminOnly)
- GET `/api/v1/movies/listMovies` — Listar todas las películas. (público)
- PUT `api/v1/movies/update/{id}` — Actualizar película por id. (AdminOnly)
- GET `api/v1/movies/details/{id}` — Obtener detalle (UserOnly)
- POST `/api/v1/movies/delete/{id}` — Eliminar película por id. (AdminOnly)


### NOTAS ADICIONALES:
El CRON decidi ponerlo en la capa de Application, pero podria ir en una capa aparte si se quisiera separar la logica de negocio de las tareas programadas.
El CRON se ejecuta una vez al dia, o tiene la opcion de ejecutarse manualmente atraves de un endpoint.
Decidi usar SQLite para no tener que utilizar una base de datos en memoria.

Las entidades heredan de una clase llamada AuditableEntity, incluye propiedades comunes como LastChangedBy y LastChangedAt 
para llevar un registro de quién y cuándo se realizaron cambios en las entidades.
Esto es útil para auditoría y seguimiento de cambios en la base de datos.
Tambien AuditableEntity hereda de una clase base llamada BaseEntity, que incluye una propiedad Id de tipo Guid.

En el AppDbContext, se hace un override de "SaveChangesAsync" para establecer automáticamente las propiedades de auditoría
(LastChangedBy y LastChangedAt) antes de guardar los cambios en la base de datos.
