import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { EstadoRemision, NuevaRemision, RemisionDetalle, RemisionResumen, Resumen } from '../models/models';

@Injectable({ providedIn: 'root' })
export class RemisionService {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/remisiones`;

  getRemisiones(estado?: string, busqueda?: string): Observable<RemisionResumen[]> {
    let params = new HttpParams();
    if (estado && estado !== 'todos') params = params.set('estado', estado);
    if (busqueda) params = params.set('busqueda', busqueda);
    return this.http.get<RemisionResumen[]>(this.url, { params });
  }

  getRemision(id: number): Observable<RemisionDetalle> {
    return this.http.get<RemisionDetalle>(`${this.url}/${id}`);
  }

  crear(remision: NuevaRemision): Observable<RemisionDetalle> {
    return this.http.post<RemisionDetalle>(this.url, remision);
  }

  // Al marcar "entregado" la API descuenta el inventario
  cambiarEstado(id: number, estado: EstadoRemision): Observable<RemisionDetalle> {
    return this.http.patch<RemisionDetalle>(`${this.url}/${id}/estado`, { estado });
  }

  getResumen(): Observable<Resumen> {
    return this.http.get<Resumen>(`${environment.apiUrl}/resumen`);
  }
}
