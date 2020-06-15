using Application.Bendahara;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class BendController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetBend")]
    public async Task<IActionResult> Get(string id) =>
      Ok(await Mediator.Send(new Detail.Query { IdBend = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetBend", new { id = request.IdBend }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdBend = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) =>
      Ok(await Mediator.Send(new Delete.Command { IdBend = id }));
  }
}