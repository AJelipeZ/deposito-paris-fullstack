-- =============================================================
-- Depósito París - Sistema de despacho de mercancía
-- Script DDL (SQLite 3)
-- =============================================================
PRAGMA foreign_keys = ON;

-- Conductores que realizan los despachos
CREATE TABLE IF NOT EXISTS conductores (
    id      INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre  TEXT    NOT NULL UNIQUE
);

-- Inventario de bodega
CREATE TABLE IF NOT EXISTS articulos (
    id      INTEGER PRIMARY KEY,
    nombre  TEXT    NOT NULL,
    stock   INTEGER NOT NULL DEFAULT 0 CHECK (stock >= 0)
);

-- Remisiones (documentos de despacho)
CREATE TABLE IF NOT EXISTS remisiones (
    id             INTEGER PRIMARY KEY,
    cliente        TEXT    NOT NULL CHECK (length(cliente) >= 3),
    telefono       TEXT    NOT NULL CHECK (length(telefono) = 10 AND substr(telefono, 1, 1) = '3'),
    fecha          TEXT    NOT NULL,                       -- formato YYYY-MM-DD
    estado         TEXT    NOT NULL DEFAULT 'pendiente'
                   CHECK (estado IN ('pendiente', 'despachado', 'entregado', 'rechazado')),
    conductor_id   INTEGER NOT NULL REFERENCES conductores (id),
    observaciones  TEXT    NOT NULL DEFAULT '',
    creada_en      TEXT    NOT NULL DEFAULT (datetime('now', 'localtime')),
    actualizada_en TEXT    NOT NULL DEFAULT (datetime('now', 'localtime'))
);

-- Artículos de cada remisión
CREATE TABLE IF NOT EXISTS remision_items (
    id           INTEGER PRIMARY KEY AUTOINCREMENT,
    remision_id  INTEGER NOT NULL REFERENCES remisiones (id) ON DELETE CASCADE,
    articulo_id  INTEGER NOT NULL REFERENCES articulos (id),
    cantidad     INTEGER NOT NULL CHECK (cantidad > 0),
    precio       INTEGER NOT NULL CHECK (precio > 0)       -- pesos colombianos, sin decimales
);

CREATE INDEX IF NOT EXISTS ix_remisiones_estado    ON remisiones (estado);
CREATE INDEX IF NOT EXISTS ix_remisiones_cliente   ON remisiones (cliente);
CREATE INDEX IF NOT EXISTS ix_items_remision       ON remision_items (remision_id);
CREATE INDEX IF NOT EXISTS ix_items_articulo       ON remision_items (articulo_id);
