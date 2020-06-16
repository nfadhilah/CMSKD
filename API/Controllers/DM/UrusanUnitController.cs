using System.Threading.Tasks;
using Application.DM.Urusan;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
  [Route("api/unit/{idUnit}/urusan")]
  public class UrusanUnitController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get(
      long idUnit, [FromQuery] PaginationQuery query) => Ok(
      await Mediator.Send(new List.Query
      {
        CurrentPage = query.CurrentPage,
        PageSize = query.PageSize,
        IdUnit = idUnit
      }));

    [HttpGet("{urusKey}", Name = "GetUrusanUnit")]
    public async Task<IActionResult> Get(
      long idUnit, long urusKey) => Ok(await Mediator.Send(new Detail.Query
      {
        IdUnit = idUnit,
        UrusKey = urusKey
      }));

    [HttpPost]
    public async Task<IActionResult> Post(long idUnit, [FromBody] Create.DTO dto)
    {
      var command = dto.MapDTO(new Create.Command { IdUnit = idUnit });
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetUrusanUnit", new { idUnit = request.IdUnit, urusKey = request.UrusKey },
        request);
    }

    [HttpPut("{urusKey}")]
    public async Task<IActionResult> Update(
      long idUnit, long urusKey, [FromBody] Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command
      { IdUnit = idUnit, UrusKey = urusKey });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{urusKey}")]
    public async Task<IActionResult> Delete(long idUnit, long urusKey)
    {
      return
        Ok(await Mediator.Send(new Delete.Command
        { IdUnit = idUnit, UrusKey = urusKey }));
    }
  }
}