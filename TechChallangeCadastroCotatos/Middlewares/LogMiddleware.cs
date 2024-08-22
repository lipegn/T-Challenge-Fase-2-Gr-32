using System.Diagnostics;
using System.Text;

namespace TechChallangeCadastroContatosAPI.Middlewares
{    
    /// <summary>
    /// Gravar log no output das informações enviadas na requisição
    /// </summary>
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// Capturar toda requisição e gravar no output
        /// </summary>
        /// <param name="httpContext">Contexto da requisição</param>
        /// <returns>Apontamento para o próximo middleware</returns>
        public async Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;
            if (request.ContentLength > 0)
            {
                request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer);
                string requestContent = Encoding.UTF8.GetString(buffer);
                // gravar requisição em json no output do visual studio
                Debug.WriteLine(requestContent);
                request.Body.Position = 0; 
            }
            await _next(httpContext);
        }
    }

    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
