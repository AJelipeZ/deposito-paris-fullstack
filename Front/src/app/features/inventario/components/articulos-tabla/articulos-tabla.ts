import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Articulo } from '../../../../core/models/models';

// Componente de presentación: muestra la tabla que le pasen
@Component({
  selector: 'app-articulos-tabla',
  standalone: false,
  styleUrl: './articulos-tabla.scss',
  templateUrl: './articulos-tabla.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ArticulosTabla {
  @Input() articulos: Articulo[] = [];
  columnas = ['id', 'nombre', 'stock'];

  claseStock(stock: number): string {
    if (stock <= 0) return 'agotado';
    if (stock <= 10) return 'bajo';
    return 'ok';
  }
}
