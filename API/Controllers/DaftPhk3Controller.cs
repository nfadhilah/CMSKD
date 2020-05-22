using Application.Rekanan;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class DaftPhk3Controller : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "Detail")]
    public async Task<IActionResult> Get(string id) =>
      Ok(await Mediator.Send(new Detail.Query { KdP3 = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("Detail", new { id = request.KdP3 }, request);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Update.Command command)
    {
      command.KdP3 = id;
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) =>
      Ok(await Mediator.Send(new Delete.Command { KdP3 = id }));
  }
}