using Application.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebApi.Middleware {
    public class ExceptionMiddleware {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = BaseResponse<string>.Fail(ex.Message);
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
