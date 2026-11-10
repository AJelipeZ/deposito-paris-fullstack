# Back · API REST Depósito París

API en **C# .NET 10** con arquitectura **por capas + CQRS** y **Dapper** sobre **SQLite**.

## Proyectos

| Proyecto | Capa | Contenido |
|---|---|---|
| `DepositoParis.Domain` | Dominio | Entidades (`Remision`, `ItemRemision`, `Articulo`), reglas de estado y excepciones de negocio. |
| `DepositoParis.Application` | Aplicación | CQRS: comandos y consultas con sus manejadores, DTOs, contratos de repositorios y el despachador (`IDispatcher`). |
| `DepositoParis.Infrastructure` | Infraestructura | Repositorios con Dapper (lectura y escritura separados), conexión SQLite e inicialización de la base de datos. |
| `DepositoParis.Api` | Presentación | Controladores REST, middleware de errores (JSON ProblemDetails), CORS y Swagger. |

Dependencias: `Api → Application, Infrastructure` · `Infrastructure → Application, Domain` · `Application → Domain`.

## CQRS

- **Commands (escritura):** `CrearRemisionCommand`, `CambiarEstadoRemisionCommand` → usan `IRemisionWriteRepository`.
- **Queries (lectura):** `ListarRemisionesQuery`, `ObtenerRemisionQuery`, `ListarArticulosQuery`, `ListarConductoresQuery`, `ObtenerResumenQuery` → usan repositorios de lectura que devuelven DTOs.

## Endpoints

| Método | Ruta | Descripción |
|---|---|---|
| GET | `/api/resumen` | Totales por estado y artículos con stock bajo |
| GET | `/api/remisiones?estado=&busqueda=` | Lista con filtros |
| GET | `/api/remisiones/{id}` | Detalle con artículos |
| POST | `/api/remisiones` | Crea una remisión (estado pendiente) |
| PATCH | `/api/remisiones/{id}/estado` | Cambia el estado; al entregar descuenta inventario |
| GET | `/api/articulos?busqueda=&soloStockBajo=` | Inventario |
| GET | `/api/conductores` | Conductores |

Respuestas en JSON. Errores: `400` datos inválidos, `404` no encontrado, `409` regla de negocio.

## Ejecutar

```bash
cd DepositoParis.Api
dotnet run
```

Swagger: http://localhost:5080/swagger · Ejemplos de peticiones en `DepositoParis.Api/DepositoParis.Api.http`.
