using Application.UnitOrganisasi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  [AllowAnonymous]
  public class DaftUnitController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get() =>
      Ok(await Mediator.Send(new List.Query()));

    [HttpGet("{id}", Name = "GetDaftUnit")]
    public async Task<IActionResult> Get(int id) =>
      Ok(await Mediator.Send(new Detail.Query { IdUnit = id }));

    [HttpGet("nodes")]
    public async Task<IActionResult> GetNode([FromQuery] Node.Query query) =>
      Ok(
        await Mediator.Send(new Node.Query
        { KdLevel = query.KdLevel, KdUnit = query.KdUnit }));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetDaftUnit", new { id = request.IdUnit }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command());
      command.IdUnit = id;
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
      Ok(await Mediator.Send(new Delete.Command { IdUnit = id }));
  }
}