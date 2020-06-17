using System.Threading.Tasks;
using Application.DM.JabTtdCQ;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
  public class JabTtdController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{idTtd}", Name = "GetJabTtd")]
    public async Task<IActionResult> Get(int idTtd) =>
      Ok(await Mediator.Send(new Detail.Query { IdTtd = idTtd }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetJabTtd", new { idTtd = request.IdTtd }, request);
    }

    [HttpPut("{idTtd}")]
    public async Task<IActionResult> Update(int idTtd, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdTtd = idTtd });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{idTtd}")]
    public async Task<IActionResult> Delete(int idTtd) =>
      Ok(await Mediator.Send(new Delete.Command { IdTtd = idTtd }));
  }
}
