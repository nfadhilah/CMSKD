using Application.Auth.RoleMenu;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Auth
{
  public class WebOtorController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("current")]
    public async Task<IActionResult> Current() =>
      Ok(await Mediator.Send(new CurrentMenu.Query()));
  }
}
