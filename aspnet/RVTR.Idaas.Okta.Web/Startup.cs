using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace RVTR.Idaas.Okta.Web
{
  public class Startup
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDistributedMemoryCache();
      services.AddSingleton<HttpHandler>();

      services.AddSession(options =>
      {
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.Name = "rvtr_idaas";
        options.Cookie.SameSite = SameSiteMode.None;
        options.IdleTimeout = new TimeSpan(0, 15, 0);
        options.IOTimeout = new TimeSpan(0, 1, 0);
      });
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="handler"></param>
    public void Configure(IApplicationBuilder builder, HttpHandler handler)
    {
      builder.UseSession();
      builder.UseRouting();

      builder.Use(async (ctx, next) =>
      {
        await ctx.Session.LoadAsync();
        await next();
      });

      builder.Use(async (ctx, next) =>
      {
        if (!ctx.Session.Keys.Contains("state") && ctx.Request.Headers.TryGetValue("X-Forwarded-Host", out StringValues forwardedHost))
        {
          ctx.Session.SetString("state", $"{ctx.Request.Headers["X-Forwarded-Proto"]}://{ctx.Request.Headers["X-Forwarded-Host"]}{ctx.Request.Headers["X-Forwarded-Uri"]}");
        }

        if (!ctx.Session.Keys.Contains("state"))
        {
          ctx.Session.SetString("state", $"{ctx.Request.Scheme}://{ctx.Request.Host.Value}{ctx.Request.Path}");
        }

        await next();
      });

      builder.UseEndpoints(endpoints =>
      {
        endpoints.Map("/", handler.UseTraefik);
        endpoints.Map("/forward/auth", handler.UseOkta);
      });
    }
  }
}
