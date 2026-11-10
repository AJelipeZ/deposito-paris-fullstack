import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Articulo, Conductor } from '../models/models';

@Injectable({ providedIn: 'root' })
export class InventarioService {
  private http = inject(HttpClient);

  getArticulos(busqueda = '', soloStockBajo = false): Observable<Articulo[]> {
    let params = new HttpParams();
    if (busqueda) params = params.set('busqueda', busqueda);
    if (soloStockBajo) params = params.set('soloStockBajo', true);
    return this.http.get<Articulo[]>(`${environment.apiUrl}/articulos`, { params });
  }

  getConductores(): Observable<Conductor[]> {
    return this.http.get<Conductor[]>(`${environment.apiUrl}/conductores`);
  }
}
