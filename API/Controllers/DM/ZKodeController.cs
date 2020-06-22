using Application.DM.ZKodeCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.DM
{
  public class ZKodeController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetZKode")]
    public async Task<IActionResult> Get(int id) =>
      Ok(await Mediator.Send(new Detail.Query { IdxKode = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetZKode", new { id = request.IdxKode }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdxKode = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
      Ok(await Mediator.Send(new Delete.Command { IdxKode = id }));
  }
}
