using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace RVTR.Idaas.Okta.Web
{
  /// <summary>
  ///
  /// </summary>
  public class HttpHandler
  {
    private readonly IConfiguration _configuration;

    /// <summary>
    ///
    /// </summary>
    /// <param name="configuration"></param>
    public HttpHandler(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task UseTraefik(HttpContext context)
    {
      if (context.Session.TryGetValue("code", out byte[] code) && context.Session.TryGetValue("state", out byte[] state))
      {
        context.Response.Headers.Add("X-Forwarded-User", "RVTR");
        context.Response.StatusCode = StatusCodes.Status200OK;

        await context.Response.WriteAsync(string.Empty);
      }
      else
      {
        await Task.Run(() =>
        {
          context.Response.Redirect("/forward/auth");
        });
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task UseOkta(HttpContext context)
    {
      var state = context.Session.GetString("state");

      if (context.Request.HasFormContentType &&
          context.Request.Form.Keys.Any(k => context.Request.Form[k] == state))
      {
        await Task.Run(() =>
        {
          context.Session.SetString("code", context.Request.Form["code"]);
          context.Response.Redirect("/");
        });
      }
      else
      {
        await Task.Run(() =>
        {
          var clientId = _configuration["Identity:ClientId"];
          var provider = _configuration["Identity:Provider"];
          var redirectUri = _configuration["Identity:RedirectUri"];

          context.Response.Redirect($"{provider}/authorize?client_id={clientId}&redirect_uri={redirectUri}&response_mode=form_post&response_type=code&scope=openid email&state={state}");
        });

      }
    }
  }
}
