using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
      services.AddSingleton<ServerHandler>();

      services.AddSession(options =>
      {
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.Name = "rvtr_idaas";
        options.Cookie.SameSite = SameSiteMode.None;
        options.IdleTimeout = new TimeSpan(0, 10, 0);
        options.IOTimeout = new TimeSpan(0, 1, 0);
      });
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="handler"></param>
    public void Configure(IApplicationBuilder builder, ServerHandler handler)
    {
      builder.UseSession();
      builder.UseRouting();

      builder.Use(async (ctx, next) =>
      {
        await ctx.Session.LoadAsync();
        await next();
      });

      builder.UseEndpoints(endpoints =>
      {
        endpoints.MapGet("/forward/auth", handler.UseTraefik);
      });
    }
  }
}
