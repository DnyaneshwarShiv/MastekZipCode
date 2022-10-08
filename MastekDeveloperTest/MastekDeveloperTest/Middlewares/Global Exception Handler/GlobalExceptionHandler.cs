using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MastekDeveloperTest.Middlewares.Global_Exception_Handler
{
    public class AppLogger
    {
    }
    /// <summary>
    /// Global Error Model
    /// </summary>
    public class GlobalErrorModel
    {
        /// <summary>
        /// Error Code
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }
    public static class GlobalExceptionConstant
    {
        public const string CONTENT_TYPE = "application/json; charset=utf-8";
        public static readonly string LOG_MSG_FORMAT = "Error Message: {0} StackTrace: {1}";
        public static readonly string ERROR_DETAILS_FORMAT = "Technical Error: {0} Stack Trace: {1}";
        public const string GENERIC_MSG = "<b>We couldn't find that page. Our bad.</b> Please try again later or contact Avalara support.";
        public const string XSS_ERROR = "Anchor tags and scripts are not allowed. Remove special characters such as <, !, or & from the details your have entered on this page.";
    }
    public static class GlobalExceptionHandler
    {
        private static ILogger<AppLogger> _logger;
        private static HashSet<Type> _businessExceptions = new HashSet<Type>();
        public static string GetExceptionMessages(this Exception e, string msgs = "")
        {
            if (e == null) return string.Empty;
            if (msgs == "") msgs = e.Message;
            if (e.InnerException != null)
                msgs += "\r\nInnerException: " + GetExceptionMessages(e.InnerException);
            return msgs;
        }
        /// <summary>
        /// Development Response
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext);
        /// <summary>
        /// Use Custom Errors
        /// </summary>
        /// <param name="app"></param>
        /// <param name="environment"></param>
        /// <param name="loggerFactory"></param>
        public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment, ILoggerFactory loggerFactory)
        {
            app.Use(WriteDevelopmentResponse);
            _logger = loggerFactory.CreateLogger<AppLogger>();
          
        }
        /// <summary>
        /// Write Response
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        private static async Task WriteResponse(HttpContext httpContext)
        {
            httpContext.Response.ContentType = GlobalExceptionConstant.CONTENT_TYPE;
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error;

            if (ex != null)
            {
                var exceptionDescription =  string.Format(GlobalExceptionConstant.ERROR_DETAILS_FORMAT, ex.GetExceptionMessages(), ex.StackTrace.ToString());
                var IsUserExceptions = _businessExceptions.Contains(ex.GetType());

                GlobalErrorModel exceptionModel;


                if (IsUserExceptions)
                {
                    exceptionModel = new GlobalErrorModel()
                    {
                        ErrorCode = StatusCodes.Status400BadRequest,
                        Description = ex.Message
                    };
                    _logger.LogInformation(string.Format(GlobalExceptionConstant.LOG_MSG_FORMAT, ex.GetExceptionMessages(), ex.StackTrace));
                }
                else
                {
                    exceptionModel = new GlobalErrorModel()
                    {
                        ErrorCode = StatusCodes.Status500InternalServerError,
                        Description = exceptionDescription
                    };
                    _logger.LogError(string.Format(GlobalExceptionConstant.ERROR_DETAILS_FORMAT, ex.GetExceptionMessages(), ex.StackTrace));
                }

                var stream = httpContext.Response.Body;
                httpContext.Response.StatusCode = IsUserExceptions ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
                await JsonSerializer.SerializeAsync(stream, exceptionModel);
            }
        }


    }
}
