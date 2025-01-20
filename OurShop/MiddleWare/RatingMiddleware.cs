using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Services;
using System.IO;
using System;
using System.Threading.Tasks;

namespace OurShop.MiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;

        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task  Invoke(HttpContext httpContext, IRatingService ratingService)
        {
            Rating newRating = new Rating()
            {

                Host = httpContext.Request.Host.ToString(),
                Method = httpContext.Request.Method.ToString(),
                Path = httpContext.Request.Path.ToString(),
                Referer = httpContext.Request.Headers["Referer"].ToString(),
                UserAgent = httpContext.Request.Headers["User-Agent"].ToString(),
                RecordDate = DateTime.UtcNow
            };
             await ratingService.addRating(newRating);
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}
