using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crusha.Core
{
    public class UploadServerMiddleware
    {
        private const string uploadPath = "uploadedFiles";
        private readonly RequestDelegate _next;

        public UploadServerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            bool processRequest = context.Request.Path.Equals("/uploadfiles", StringComparison.CurrentCultureIgnoreCase);
            if (processRequest && 
                context.Request.ContentLength > 0)
            {
                return ProcessForm(context);

            }
            else
            {
                // Call the next delegate/middleware in the pipeline
                return this._next(context);
            }
        }

        private async Task ProcessForm(HttpContext context)
        {
            var form = await context?.Request?.ReadFormAsync();

            foreach (var file in form?.Files)
            {
                using (Stream output = File.OpenWrite($"{uploadPath}/{DateTime.UtcNow.Ticks}_{file.FileName}"))
                {
                    await file.CopyToAsync(output);
                }
            }
        }
    }

    public static class UploadServerMiddlewareExtensions
    {
        public static IApplicationBuilder UseUploadServer(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UploadServerMiddleware>();
        }
    }
}
