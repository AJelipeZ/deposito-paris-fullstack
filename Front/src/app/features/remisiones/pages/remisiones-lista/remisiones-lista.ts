import { ChangeDetectionStrategy, Component, OnDestroy, OnInit, inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, combineLatest, debounceTime, startWith, switchMap, takeUntil, tap } from 'rxjs';
import { RemisionResumen } from '../../../../core/models/models';
import { mensajeError } from '../../../../core/services/error-api';
import { RemisionService } from '../../../../core/services/remision.service';

@Component({
  selector: 'app-remisiones-lista',
  standalone: false,
  styleUrl: './remisiones-lista.scss',
  templateUrl: './remisiones-lista.html',
  changeDetection: ChangeDetectionStrategy.Eager,
})
export class RemisionesLista implements OnInit, OnDestroy {
  private remisionService = inject(RemisionService);
  private router = inject(Router);
  private destroy$ = new Subject<void>();

  remisiones: RemisionResumen[] = [];
  cargando = true;
  error = '';
  busqueda = new FormControl('', { nonNullable: true });
  filtroEstado = new FormControl('todos', { nonNullable: true });

  ngOnInit(): void {
    // Cada vez que cambia la búsqueda o el estado se consulta la API
    combineLatest([
      this.busqueda.valueChanges.pipe(startWith(''), debounceTime(300)),
      this.filtroEstado.valueChanges.pipe(startWith('todos')),
    ])
      .pipe(
        tap(() => (this.cargando = true)),
        switchMap(([busqueda, estado]) => this.remisionService.getRemisiones(estado, busqueda.trim())),
        takeUntil(this.destroy$),
      )
      .subscribe({
        next: (datos) => {
          this.remisiones = datos;
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

  verDetalle(id: number): void {
    this.router.navigate(['/remisiones', id]);
  }
}
