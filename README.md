# Depósito París · Sistema de despacho de mercancía

Aplicación web completa para gestionar las **remisiones** (pedidos que salen de bodega) y el **inventario** de una ferretería en Medellín.

```
/
├── Front   → Aplicación web en Angular 22 + Angular Material
├── Back    → API REST en C# .NET 10 (Capas + CQRS + Dapper)
└── BD      → Scripts de la base de datos SQLite (DDL y datos iniciales)
```

## Requisitos

| Herramienta | Versión |
|---|---|
| .NET SDK | 9.0 o superior (probado con 10.0) |
| Node.js | 22.22+ o 24.15+ |
| npm | 10 o superior |

No hace falta instalar un motor de base de datos: SQLite viene incluido con la API y el archivo se crea solo.

## Cómo ejecutarlo

**1. Backend** (terminal 1):

```bash
cd Back/DepositoParis.Api
dotnet run
```

La API queda en http://localhost:5080 y la documentación Swagger en http://localhost:5080/swagger.
La primera vez crea el archivo `deposito_paris.db` con las tablas y los datos de prueba (scripts de `/BD`).

**2. Frontend** (terminal 2):

```bash
cd Front
npm install
npx ng serve
```

Abrir http://localhost:4200

> Para volver a los datos iniciales: detener la API, borrar `Back/DepositoParis.Api/deposito_paris.db` y ejecutarla otra vez.
