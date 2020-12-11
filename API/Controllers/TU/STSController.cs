using Application.TU.STSCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.TU
{
  public class STSController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) => Ok(await Mediator.Send(query));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Create.Command command) => Ok(await Mediator.Send(command));
  }
}