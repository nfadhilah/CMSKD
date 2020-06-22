using Application.MA.SPDCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.MA
{
  public class SPDController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetSPD")]
    public async Task<IActionResult> Get(long id) =>
      Ok(await Mediator.Send(new Detail.Query { IdSPD = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetSPD", new { id = request.IdSPD }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdSPD = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id) =>
      Ok(await Mediator.Send(new Delete.Command { IdSPD = id }));
  }
}