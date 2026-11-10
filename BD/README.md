# BD · Base de datos SQLite

| Archivo | Contenido |
|---|---|
| `01_ddl_deposito_paris.sql` | Script DDL: tablas `conductores`, `articulos`, `remisiones`, `remision_items`, llaves foráneas, restricciones CHECK e índices. |
| `02_datos_iniciales.sql` | Datos de prueba (3 conductores, 20 artículos, 6 remisiones). |

La API ejecuta estos scripts automáticamente la primera vez. Para crear la base manualmente:

```bash
sqlite3 deposito_paris.db < 01_ddl_deposito_paris.sql
sqlite3 deposito_paris.db < 02_datos_iniciales.sql
```
