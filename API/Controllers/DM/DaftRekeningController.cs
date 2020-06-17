using System.Threading.Tasks;
using Application.DM.DaftRekeningCQ;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
  public class DaftRekeningController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{idRek}", Name = "GetDaftRekening")]
    public async Task<IActionResult> Get(int idRek) =>
      Ok(await Mediator.Send(new Detail.Query { IdRek = idRek }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetDaftRekening", new { idRek = request.IdRek }, request);
    }

    [HttpPut("{idRek}")]
    public async Task<IActionResult> Update(int idRek, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdRek = idRek });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{idRek}")]
    public async Task<IActionResult> Delete(int idRek) =>
      Ok(await Mediator.Send(new Delete.Command { IdRek = idRek }));
  }
}
