using Application.MappingKPA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  [AllowAnonymous]
  [Route("api/unit/{idUnit}/pegawai")]
  public class KPAController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get(long idUnit) =>
      Ok(await Mediator.Send(new List.Query { IdUnit = idUnit }));

    [HttpGet("{idPeg}", Name = "GetKPA")]
    public async Task<IActionResult> Get(
      long idUnit, long idPeg) => Ok(await Mediator.Send(new Detail.Query
      {
        IdUnit = idUnit,
        IdPeg = idPeg
      }));

    [HttpPost]
    public async Task<IActionResult> Post(long idUnit, [FromBody] Create.DTO dto)
    {
      var command = dto.MapDTO(new Create.Command { IdUnit = idUnit });
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetKPA", new { idUnit = request.IdUnit, idPeg = request.IdPeg },
        request);
    }

    [HttpPut("{idPeg}")]
    public async Task<IActionResult> Update(
      long idUnit, long idPeg, [FromBody] Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command
      { IdUnit = idUnit, IdPeg = idPeg });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{idPeg}")]
    public async Task<IActionResult> Delete(long idUnit, long idPeg)
    {
      return
        Ok(await Mediator.Send(new Delete.Command
        { IdUnit = idUnit, IdPeg = idPeg }));
    }
  }
}
