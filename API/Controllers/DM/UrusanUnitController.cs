using Application.DM.UrusanUnitCQ;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.DM
{
  public class UrusanUnitController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get(
      [FromQuery] PaginationQuery query) => Ok(
      await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetUrusanUnit")]
    public async Task<IActionResult> Get(
      long id) => Ok(await Mediator.Send(new Detail.Query
      {
        IdUrusanUnit = id
      }));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetUrusanUnit", new { id = request.IdUrusanUnit, urusKey = request.IdUrus },
        request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
      long id, [FromBody] Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command
      { IdUrusanUnit = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
      return
        Ok(await Mediator.Send(new Delete.Command
        { IdUrusanUnit = id }));
    }
  }
}