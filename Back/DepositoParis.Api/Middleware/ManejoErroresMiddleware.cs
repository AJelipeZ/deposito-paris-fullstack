using DepositoParis.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DepositoParis.Api.Middleware;

/// <summary>Convierte las excepciones en respuestas JSON (ProblemDetails) con el código HTTP correcto.</summary>
public class ManejoErroresMiddleware(RequestDelegate next, ILogger<ManejoErroresMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var (status, titulo) = ex switch
            {
                ValidacionException => (StatusCodes.Status400BadRequest, "Datos no válidos"),
                NoEncontradoException => (StatusCodes.Status404NotFound, "No encontrado"),
                ReglaNegocioException => (StatusCodes.Status409Conflict, "Regla de negocio"),
                _ => (StatusCodes.Status500InternalServerError, "Error interno"),
            };

            if (status == StatusCodes.Status500InternalServerError)
            {
                logger.LogError(ex, "Error no controlado");
            }

            var problema = new ProblemDetails
            {
                Status = status,
                Title = titulo,
                Detail = status == StatusCodes.Status500InternalServerError ? "Ocurrió un error inesperado." : ex.Message,
            };
            if (ex is ValidacionException validacion)
            {
                problema.Extensions["errores"] = validacion.Errores;
            }

            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(problema);
        }
    }
}
