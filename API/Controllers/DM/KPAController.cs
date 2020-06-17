using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DM.KPACQ;

namespace API.Controllers.DM
{
  public class KPAController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetKPA")]
    public async Task<IActionResult> Get(
      long id) => Ok(await Mediator.Send(new Detail.Query
      {
        IdKPA = id
      }));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Create.DTO dto)
    {
      var command = dto.MapDTO(new Create.Command());
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetKPA", new { id = request.IdKPA },
        request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
      long id, [FromBody] Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command
      { IdKPA = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
      return
        Ok(await Mediator.Send(new Delete.Command
        { IdKPA = id }));
    }
  }
}
