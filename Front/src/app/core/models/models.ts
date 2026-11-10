// Modelos que devuelve la API (camelCase en JSON)

export type EstadoRemision = 'pendiente' | 'despachado' | 'entregado' | 'rechazado';

export interface RemisionResumen {
  id: number;
  cliente: string;
  telefono: string;
  fecha: string;
  estado: EstadoRemision;
  conductor: string;
  cantidadItems: number;
  total: number;
}

export interface ItemRemision {
  articuloId: number;
  descripcion: string;
  cantidad: number;
  precio: number;
  subtotal: number;
}

export interface RemisionDetalle {
  id: number;
  cliente: string;
  telefono: string;
  fecha: string;
  estado: EstadoRemision;
  conductorId: number;
  conductor: string;
  observaciones: string;
  total: number;
  items: ItemRemision[];
}

export interface NuevaRemision {
  cliente: string;
  telefono: string;
  conductorId: number;
  observaciones: string;
  items: { articuloId: number; cantidad: number; precio: number }[];
}

export interface Articulo {
  id: number;
  nombre: string;
  stock: number;
}

export interface Conductor {
  id: number;
  nombre: string;
}

export interface Resumen {
  pendientes: number;
  despachadas: number;
  entregadas: number;
  rechazadas: number;
  articulosStockBajo: number;
}
