using System.Collections.Generic;
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
    private readonly Dictionary<string, Vault> _sessionstate;
    private readonly IConfiguration _configuration;

    /// <summary>
    ///
    /// </summary>
    /// <param name="configuration"></param>
    public HttpHandler(IConfiguration configuration)
    {
      _configuration = configuration;
      _sessionstate = new Dictionary<string, Vault>();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task UseOkta(HttpContext context)
    {
      var state = context.Request.HasFormContentType ? context.Request.Form["state"].ToString() : context.Session.GetString("state");

      if (context.Request.HasFormContentType &&
          context.Request.Form.ContainsKey("code") &&
          context.Request.Form["state"] == state &&
          _sessionstate.ContainsKey(state))
      {
        await Task.Run(() =>
        {
          _sessionstate[state].Code = context.Request.Form["code"];
          context.Response.Redirect($"{_sessionstate[state].Root}{_sessionstate[state].Path}");
        });

        return;
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task UseTraefik(HttpContext context)
    {
      var state = context.Session.GetString("state");

      if (_sessionstate.ContainsKey(state) &&
          !string.IsNullOrEmpty(_sessionstate[state].Code))
      {
        context.Response.Headers.Add("X-Forwarded-User", "RVTR");
        context.Response.StatusCode = StatusCodes.Status200OK;

        await context.Response.WriteAsync(string.Empty);
      }
      else
      {
        await Task.Run(() =>
        {
          var clientId = _configuration["IDAAS:ClientId"];
          var provider = _configuration["IDAAS:Provider"];
          var redirectUri = _configuration["IDAAS:RedirectUri"];
          var state = context.Session.GetString("state");

          _sessionstate.Add(state, new Vault { Path = context.Items["path"].ToString(), Root = context.Items["root"].ToString() });
          context.Response.Redirect($"{provider}/authorize?client_id={clientId}&redirect_uri={redirectUri}&response_mode=form_post&response_type=code&scope=openid email&state={state}");
        });
      }
    }

    /// <summary>
    ///
    /// </summary>
    private class Vault
    {
      /// <summary>
      ///
      /// </summary>
      /// <value></value>
      public string Code { get; set; }

      /// <summary>
      ///
      /// </summary>
      /// <value></value>
      public string Path { get; set; }

      /// <summary>
      ///
      /// </summary>
      /// <value></value>
      public string Root { get; set; }
    }
  }
}
