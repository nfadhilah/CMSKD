using Application.DM.StatTrsCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.DM
{
  public class StatTrsController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetKodeStatTrs")]
    public async Task<IActionResult> Get(string id) =>
      Ok(await Mediator.Send(new Detail.Query { KdStatus = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetKodeStatTrs", new { id = request.KdStatus }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { KdStatus = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) =>
      Ok(await Mediator.Send(new Delete.Command { KdStatus = id }));
  }
}
