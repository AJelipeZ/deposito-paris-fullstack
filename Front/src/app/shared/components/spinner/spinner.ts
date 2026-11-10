import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-spinner',
  standalone: false,
  styleUrl: './spinner.scss',
  templateUrl: './spinner.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Spinner {
  @Input() mensaje = 'Cargando...';
}
