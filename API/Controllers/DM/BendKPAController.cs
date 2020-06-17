using System.Threading.Tasks;
using Application.DM.BendKPACQ;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
  public class BendKPAController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{idBendKPA}", Name = "GetBendKPA")]
    public async Task<IActionResult> Get(int idBendKPA) =>
      Ok(await Mediator.Send(new Detail.Query { IdBendKPA = idBendKPA }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetBendKPA", new { idBendKPA = request.IdBendKPA }, request);
    }

    [HttpPut("{idBendKPA}")]
    public async Task<IActionResult> Update(int idBendKPA, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdBendKPA = idBendKPA });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{idBendKPA}")]
    public async Task<IActionResult> Delete(int idBendKPA) =>
      Ok(await Mediator.Send(new Delete.Command { IdBendKPA = idBendKPA }));
  }
}
