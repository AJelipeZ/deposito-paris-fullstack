import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';

// Módulo central: aquí van los servicios únicos de la app (se importa solo en AppModule)
@NgModule({
  imports: [CommonModule],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule?: CoreModule) {
    if (parentModule) {
      throw new Error('CoreModule ya está cargado, impórtalo solo en AppModule');
    }
  }
}
