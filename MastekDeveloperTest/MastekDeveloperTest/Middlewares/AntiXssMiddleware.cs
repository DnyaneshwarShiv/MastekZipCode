using System;
using System.Threading.Tasks;

using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using MastekDeveloperTest.Middlewares.ErrorModel;
using Newtonsoft.Json;

namespace MastekDeveloperTest.Middlewares
{
    /// <summary>
    /// Anti Xss Middleware
    /// </summary>
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _statusCode = (int)HttpStatusCode.BadRequest;

        /// <summary>
        /// Constrcuter DI
        /// </summary>
        /// <param name="next"></param>
        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Middleware call Propagation
        /// checks for Request param, url and body
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            // Check XSS in URL
            if (!string.IsNullOrWhiteSpace(context.Request.Path.Value))
            {
                var url = context.Request.Path.Value;

                if (CrossSiteScriptingValidation.IsDangerousString(url, out _))
                {
                    await RespondWithAnError(context).ConfigureAwait(false);
                    return;
                }
            }

            // Check XSS in query string
            if (!string.IsNullOrWhiteSpace(context.Request.QueryString.Value))
            {
                var queryString = WebUtility.UrlDecode(context.Request.QueryString.Value);

                if (CrossSiteScriptingValidation.IsDangerousString(queryString, out _))
                {
                    await RespondWithAnError(context).ConfigureAwait(false);
                    return;
                }
            }

            // Check XSS in request content
            var originalBody = context.Request.Body;
            try
            {
                var content = await ReadRequestBody(context);

                if (CrossSiteScriptingValidation.IsDangerousString(content, out _))
                {
                    await RespondWithAnError(context).ConfigureAwait(false);
                    return;
                }
                await _next(context).ConfigureAwait(false);
            }
            finally
            {
                context.Request.Body = originalBody;
            }
        }

        /// <summary>
        /// Read Request body
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static async Task<string> ReadRequestBody(HttpContext context)
        {
            var buffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(buffer);
            context.Request.Body = buffer;
            buffer.Position = 0;

            var encoding = Encoding.UTF8;

            var requestContent = await new StreamReader(buffer, encoding).ReadToEndAsync();
            context.Request.Body.Position = 0;

            return requestContent;
        }

        private async Task RespondWithAnError(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = _statusCode;

              var  _error = new XSSErrorResponse
                {
                    Description = "Anchor tags and scripts are not allowed. Remove special characters such as <, !, or & from the details your have entered on this page.",
                    ErrorCode = StatusCodes.Status400BadRequest
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(_error));
        }
    }
}
