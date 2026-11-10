import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subject, combineLatest, debounceTime, startWith, switchMap, takeUntil, tap } from 'rxjs';
import { Articulo } from '../../../../core/models/models';
import { mensajeError } from '../../../../core/services/error-api';
import { InventarioService } from '../../../../core/services/inventario.service';

@Component({
  selector: 'app-inventario-lista',
  standalone: false,
  styleUrl: './inventario-lista.scss',
  templateUrl: './inventario-lista.html',
  changeDetection: ChangeDetectionStrategy.Eager,
})
export class InventarioLista implements OnInit, OnDestroy {
  private inventarioService = inject(InventarioService);
  private destroy$ = new Subject<void>();

  articulos: Articulo[] = [];
  cargando = true;
  error = '';
  busqueda = new FormControl('', { nonNullable: true });
  soloBajos = new FormControl(false, { nonNullable: true });

  ngOnInit(): void {
    combineLatest([
      this.busqueda.valueChanges.pipe(startWith(''), debounceTime(300)),
      this.soloBajos.valueChanges.pipe(startWith(false)),
    ])
      .pipe(
        tap(() => (this.cargando = true)),
        switchMap(([busqueda, soloBajos]) => this.inventarioService.getArticulos(busqueda.trim(), soloBajos)),
        takeUntil(this.destroy$),
      )
      .subscribe({
        next: (datos) => {
          this.articulos = datos;
          this.cargando = false;
          this.error = '';
        },
        error: (err) => {
          this.error = mensajeError(err);
          this.cargando = false;
        },
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
