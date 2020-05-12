using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [AllowAnonymous]
  public class PingController : BaseController
  {
    private readonly IHttpContextAccessor _accessor;

    public PingController(IHttpContextAccessor accessor)
    {
      _accessor = accessor;
    }

    [HttpGet]
    public IActionResult Ping()
    {
      var remoteIpAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

      return Ok(new { Message = $"You are pinging from {remoteIpAddress}" });
    }
  }
}
