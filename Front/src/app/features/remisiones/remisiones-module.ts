import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared-module';
import { RemisionesRoutingModule } from './remisiones-routing-module';
import { RemisionesLista } from './pages/remisiones-lista/remisiones-lista';
import { RemisionDetalle } from './pages/remision-detalle/remision-detalle';
import { NuevaRemision } from './pages/nueva-remision/nueva-remision';
import { RemisionCard } from './components/remision-card/remision-card';

@NgModule({
  declarations: [RemisionesLista, RemisionDetalle, NuevaRemision, RemisionCard],
  imports: [SharedModule, RemisionesRoutingModule],
})
export class RemisionesModule {}
