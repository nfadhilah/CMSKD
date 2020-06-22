using Application.TUBEND.STSDetDCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.TUBEND
{
  public class STSDetDController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetSTSDetD")]
    public async Task<IActionResult> Get(long id) =>
      Ok(await Mediator.Send(new Detail.Query { IdSTSDetD = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetSTSDetD", new { id = request.IdSTSDetD }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdSTSDetD = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id) =>
      Ok(await Mediator.Send(new Delete.Command { IdSTSDetD = id }));
  }
}