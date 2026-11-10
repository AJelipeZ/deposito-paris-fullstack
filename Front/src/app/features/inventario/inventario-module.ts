import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared-module';
import { InventarioRoutingModule } from './inventario-routing-module';
import { InventarioLista } from './pages/inventario-lista/inventario-lista';
import { ArticulosTabla } from './components/articulos-tabla/articulos-tabla';

@NgModule({
  declarations: [InventarioLista, ArticulosTabla],
  imports: [SharedModule, InventarioRoutingModule],
})
export class InventarioModule {}
