using Application.Rekanan;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class DaftPhk3Controller : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(new List.Query
      {
        Norcp3 = query.Norcp3,
        Nminst = query.Nminst,
        Nmbank = query.Nmbank,
        Nmp3 = query.Nmp3
      }));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) =>
      Ok(await Mediator.Send(new Detail.Query { Kdp3 = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command) =>
      Ok(await Mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Update.Command command)
    {
      command.Kdp3 = id;
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) =>
      Ok(await Mediator.Send(new Delete.Command { Kdp3 = id }));
  }
}