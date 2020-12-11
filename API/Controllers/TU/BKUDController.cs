using Application.TU.BKUDCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.TU
{
  public class BKUDController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) => Ok(await Mediator.Send(query));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Create.Command command) => Ok(await Mediator.Send(command));
  }
}