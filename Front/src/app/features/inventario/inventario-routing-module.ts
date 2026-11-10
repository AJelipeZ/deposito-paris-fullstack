import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InventarioLista } from './pages/inventario-lista/inventario-lista';

const routes: Routes = [{ path: '', component: InventarioLista }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class InventarioRoutingModule {}
