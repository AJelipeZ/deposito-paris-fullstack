import { provideHttpClient } from '@angular/common/http';
import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { CoreModule } from './core/core-module';
import { SharedModule } from './shared/shared-module';
import { Navbar } from './layout/navbar/navbar';
import { Inicio } from './pages/inicio/inicio';
import { NotFound } from './pages/not-found/not-found';

@NgModule({
  declarations: [App, Navbar, Inicio, NotFound],
  imports: [BrowserModule, CoreModule, SharedModule, AppRoutingModule],
  providers: [provideBrowserGlobalErrorListeners(), provideHttpClient()],
  bootstrap: [App],
})
export class AppModule {}
