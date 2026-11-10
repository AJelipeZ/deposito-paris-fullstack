import { Pipe, PipeTransform } from '@angular/core';

// Formato de pesos colombianos: 660000 -> $660.000
@Pipe({
  name: 'cop',
  standalone: false,
})
export class CopPipe implements PipeTransform {
  transform(valor: number): string {
    return '$' + valor.toLocaleString('es-CO');
  }
}
