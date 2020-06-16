using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Management;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
  public class LicenseService : IHostedService, IDisposable
  {
    private readonly ILogger<LicenseService> _logger;
    private readonly IWebHostEnvironment _env;
    private Timer _timer;

    public LicenseService(ILogger<LicenseService> logger, IWebHostEnvironment env)
    {
      _logger = logger;
      _env = env;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
      // if (!_env.IsProduction()) return Task.CompletedTask;

      _logger.LogInformation($"License Service running on HW ID: {UUID}");

      // Cek license pada saat API pertama kali run dan setiap 30 hari
      // Jika license invalid throw exception invalid license
      _timer = new Timer(DoCheck, null, TimeSpan.Zero,
        TimeSpan.FromDays(30));

      return Task.CompletedTask;
    }

    private void DoCheck(object state)
    {
      // Validate License berdasarkan HW ID
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("License Service is stopping.");

      _timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public static string UUID
    {
      get
      {
        var uuid = string.Empty;
        var mc = new ManagementClass("Win32_ComputerSystemProduct");
        var moc = mc.GetInstances();

        foreach (var o in moc)
        {
          var mo = (ManagementObject)o;
          uuid = mo.Properties["UUID"].Value.ToString();
          break;
        }

        return uuid;
      }
    }

    public void Dispose()
    {
      _timer?.Dispose();
    }
  }
}