using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RVTR.Idaas.Okta.Web
{
  /// <summary>
  ///
  /// </summary>
  public class ServerHandler
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task UseTraefik(HttpContext context)
    {
      context.Response.Headers.Add("X-Forwarded", "RVTR");
      context.Response.StatusCode = StatusCodes.Status200OK;

      await context.Response.WriteAsync(string.Empty);
    }
  }
}
