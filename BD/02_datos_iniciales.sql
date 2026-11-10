-- =============================================================
-- Depósito París - Datos iniciales de prueba (DML)
-- =============================================================
INSERT INTO conductores (nombre) VALUES ('Porfirio Villa'), ('Pedro Vargas'), ('Anderson Ayala');

INSERT INTO articulos (id, nombre, stock) VALUES
 (101, 'Arena de concreto (bulto)', 514),
 (102, 'Arena de pega (bulto)', 320),
 (103, 'Cemento gris 50kg', 230),
 (104, 'Cemento blanco 25kg', 8),
 (105, 'Varilla corrugada 1/2', 150),
 (106, 'Varilla corrugada 3/8', 0),
 (107, 'Ladrillo tolete', 2400),
 (108, 'Bloque de cemento 15', 600),
 (109, 'Tubo PVC 1/2', 85),
 (110, 'Tubo PVC sanitario 4', 6),
 (111, 'Codo PVC 1/2', 300),
 (112, 'Soldadura PVC 1/4 gal', 12),
 (113, 'Teja de zinc 3m', 40),
 (114, 'Alambre negro cal. 18 (kg)', 3),
 (115, 'Puntilla 2 pulg (kg)', 55),
 (116, 'Pintura vinilo blanco 1gal', 0),
 (117, 'Brocha 3 pulg', 25),
 (118, 'Pega cerámica 25kg', 9),
 (119, 'Gravilla 1/2 (bulto)', 170),
 (120, 'Malla electrosoldada', 18);

INSERT INTO remisiones (id, cliente, telefono, fecha, estado, conductor_id, observaciones) VALUES
 (23301, 'Construcciones El Poblado', '3104567821', '2026-11-03', 'pendiente',  2, ''),
 (23302, 'María Fernanda Restrepo',   '3157894512', '2026-11-03', 'pendiente',  1, 'Llamar antes de llegar'),
 (23303, 'Ferreobras Bello',          '3012456789', '2026-11-02', 'despachado', 3, ''),
 (23304, 'Juan Camilo Zapata',        '3206541238', '2026-11-02', 'entregado',  2, ''),
 (23305, 'Luz Dary Cardona',          '3014789632', '2026-11-01', 'rechazado',  1, 'El cliente no estaba'),
 (23306, 'Inversiones Laureles',      '3118523697', '2026-11-01', 'pendiente',  3, '');

INSERT INTO remision_items (remision_id, articulo_id, cantidad, precio) VALUES
 (23301, 103, 20, 32500),
 (23301, 101, 40, 5500),
 (23302, 107, 500, 1150),
 (23303, 109, 10, 9800),
 (23303, 111, 20, 900),
 (23304, 105, 30, 28900),
 (23305, 117, 4, 7800),
 (23306, 113, 12, 48500),
 (23306, 115, 5, 9500);
