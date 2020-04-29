using Application.Rekanan;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class DaftPhk3Controller : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get() =>
      Ok(await Mediator.Send(new List.Query()));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command) =>
      Ok(await Mediator.Send(command));
  }
}
