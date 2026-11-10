import { Pipe, PipeTransform } from '@angular/core';
import { EstadoRemision } from '../../core/models/models';

@Pipe({
  name: 'estado',
  standalone: false,
})
export class EstadoPipe implements PipeTransform {
  transform(estado: EstadoRemision): string {
    const textos = {
      pendiente: 'Pendiente',
      despachado: 'Despachado',
      entregado: 'Entregado',
      rechazado: 'Rechazado',
    };
    return textos[estado];
  }
}
