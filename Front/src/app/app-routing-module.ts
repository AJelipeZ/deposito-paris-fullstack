import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Inicio } from './pages/inicio/inicio';
import { NotFound } from './pages/not-found/not-found';

const routes: Routes = [
  { path: '', component: Inicio },
  {
    path: 'remisiones',
    loadChildren: () => import('./features/remisiones/remisiones-module').then((m) => m.RemisionesModule),
  },
  {
    path: 'inventario',
    loadChildren: () => import('./features/inventario/inventario-module').then((m) => m.InventarioModule),
  },
  { path: '**', component: NotFound },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
