using System.Net;
using System.Text.Json;

namespace TaskFlow.Api.Middleware
{
    public class ManejadorDeExcepcionesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorDeExcepcionesMiddleware> _logger;

        public ManejadorDeExcepcionesMiddleware(RequestDelegate next, ILogger<ManejadorDeExcepcionesMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado procesando {Metodo} {Ruta}", context.Request.Method, context.Request.Path);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var respuesta = new { mensaje = "Ocurrió un error inesperado. Intentá de nuevo más tarde." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(respuesta));
            }
        }
    }
}