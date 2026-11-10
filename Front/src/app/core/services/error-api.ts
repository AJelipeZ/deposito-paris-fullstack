import { HttpErrorResponse } from '@angular/common/http';

// Convierte el error de la API (ProblemDetails) en un mensaje para el usuario
export function mensajeError(error: unknown): string {
  if (error instanceof HttpErrorResponse) {
    if (error.status === 0) {
      return 'No hay conexión con la API. ¿Está corriendo el backend en http://localhost:5080?';
    }
    const errores = error.error?.errores as Record<string, string[]> | undefined;
    if (errores) {
      return Object.values(errores).flat().join(' ');
    }
    return error.error?.detail ?? 'Ocurrió un error en el servidor.';
  }
  return 'Ocurrió un error inesperado.';
}
