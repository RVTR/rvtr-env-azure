using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace RVTR.Idaas.Okta.Web
{
  /// <summary>
  ///
  /// </summary>
  public class Program
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="args"></param>
    public static void Main()
    {
      CreateHostBuilder().Build().Run();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder() =>
      Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
      {
        webBuilder.UseStartup<Startup>();
      });
  }
}
