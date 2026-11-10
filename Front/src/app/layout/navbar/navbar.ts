import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  standalone: false,
  styleUrl: './navbar.scss',
  templateUrl: './navbar.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Navbar {
  links = [
    { ruta: '/', texto: 'Inicio', icono: 'home' },
    { ruta: '/remisiones', texto: 'Remisiones', icono: 'local_shipping' },
    { ruta: '/remisiones/nueva', texto: 'Nueva remisión', icono: 'add' },
    { ruta: '/inventario', texto: 'Inventario', icono: 'inventory_2' },
  ];
}
