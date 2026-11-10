# Front · Depósito París

Aplicación en **Angular 22** (módulos, `standalone: false`) con **Angular Material**. Consume la API REST del backend.

## Pantallas

- **Inicio:** resumen de remisiones por estado y artículos con poco stock.
- **Remisiones:** búsqueda por cliente o número y filtro por estado.
- **Detalle:** artículos, total y cambio de estado con confirmación (al entregar se descuenta el inventario).
- **Nueva remisión:** formulario reactivo con validaciones y varios artículos.
- **Inventario:** búsqueda y colores según el stock.

## Estructura

- `core/`: modelos y servicios (`RemisionService`, `InventarioService`) que llaman a la API con `HttpClient`.
- `shared/`: diálogo de confirmación, spinner, pipes y módulos de Material.
- `features/remisiones` y `features/inventario`: módulos con lazy loading. En `pages` están los componentes contenedores y en `components` los de presentación.

La URL de la API está en `src/environments/environment.ts` (`http://localhost:5080/api`).

## Ejecutar

Primero levantar el backend, luego:

```bash
npm install
npx ng serve
```

Abrir http://localhost:4200
