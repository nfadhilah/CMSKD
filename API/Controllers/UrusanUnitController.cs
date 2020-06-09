using Application.Dtos;
using Application.Urusan;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
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

    [HttpGet("{idUrusanUnit}", Name = "GetUrusanUnit")]
    public async Task<IActionResult> Get(
      long idUnit, long idUrusanUnit) => Ok(await Mediator.Send(new Detail.Query
      {
        IdUnit = idUnit,
        IdUrusanUnit = idUrusanUnit
      }));

    [HttpPost]
    public async Task<IActionResult> Post(long idUnit, Create.Command command)
    {
      command.IdUnit = idUnit;
      var request = await Mediator.Send(command);

      return CreatedAtRoute("GetUrusanUnit", new { idUnit = request.IdUnit, idUrusanUnit = request.IdUrusanUnit },
        request);
    }

    [HttpPut("{idUrusanUnit}")]
    public async Task<IActionResult> Update(
      long idUnit, long idUrusanUnit, Update.Command command)
    {
      command.IdUnit = idUnit;
      command.IdUrusanUnit = idUrusanUnit;
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{idUrusanUnit}")]
    public async Task<IActionResult> Delete(long idUnit, long idUrusanUnit)
    {
      return
        Ok(await Mediator.Send(new Delete.Command
        { IdUnit = idUnit, IdUrusanUnit = idUrusanUnit }));
    }
  }
}