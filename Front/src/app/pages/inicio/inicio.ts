import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';
import { Resumen } from '../../core/models/models';
import { mensajeError } from '../../core/services/error-api';
import { RemisionService } from '../../core/services/remision.service';

@Component({
  selector: 'app-inicio',
  standalone: false,
  styleUrl: './inicio.scss',
  templateUrl: './inicio.html',
  changeDetection: ChangeDetectionStrategy.Eager,
})
export class Inicio implements OnInit {
  private remisionService = inject(RemisionService);

  cargando = true;
  error = '';
  resumen: Resumen = { pendientes: 0, despachadas: 0, entregadas: 0, rechazadas: 0, articulosStockBajo: 0 };

  ngOnInit(): void {
    this.remisionService.getResumen().subscribe({
      next: (resumen) => {
        this.resumen = resumen;
        this.cargando = false;
      },
      error: (err) => {
        this.error = mensajeError(err);
        this.cargando = false;
      },
    });
  }
}
