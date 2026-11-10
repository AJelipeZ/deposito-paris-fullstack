import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { RemisionResumen } from '../../../../core/models/models';

// Componente de presentación: solo recibe datos y emite eventos
@Component({
  selector: 'app-remision-card',
  standalone: false,
  styleUrl: './remision-card.scss',
  templateUrl: './remision-card.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RemisionCard {
  @Input({ required: true }) remision!: RemisionResumen;
  @Output() ver = new EventEmitter<number>();
}
