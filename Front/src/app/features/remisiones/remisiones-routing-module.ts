import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RemisionesLista } from './pages/remisiones-lista/remisiones-lista';
import { NuevaRemision } from './pages/nueva-remision/nueva-remision';
import { RemisionDetalle } from './pages/remision-detalle/remision-detalle';

const routes: Routes = [
  { path: '', component: RemisionesLista },
  { path: 'nueva', component: NuevaRemision },
  { path: ':id', component: RemisionDetalle },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RemisionesRoutingModule {}
