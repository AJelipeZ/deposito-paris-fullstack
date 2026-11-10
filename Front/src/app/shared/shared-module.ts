import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ConfirmDialog } from './components/confirm-dialog/confirm-dialog';
import { Spinner } from './components/spinner/spinner';
import { CopPipe } from './pipes/cop-pipe';
import { EstadoPipe } from './pipes/estado-pipe';

const MATERIAL = [
  MatButtonModule,
  MatCardModule,
  MatChipsModule,
  MatDialogModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatMenuModule,
  MatProgressSpinnerModule,
  MatSelectModule,
  MatSnackBarModule,
  MatTableModule,
  MatToolbarModule,
];

// Módulo compartido: componentes, pipes y módulos de Material que usan todas las features
@NgModule({
  declarations: [ConfirmDialog, Spinner, CopPipe, EstadoPipe],
  imports: [CommonModule, ...MATERIAL],
  exports: [CommonModule, ReactiveFormsModule, RouterModule, ...MATERIAL, ConfirmDialog, Spinner, CopPipe, EstadoPipe],
})
export class SharedModule {}
