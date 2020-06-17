using System.Threading.Tasks;
using Application.DM.NRCBendCQ;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
  public class NrcBendController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{idNrcBend}", Name = "GetNrcBend")]
    public async Task<IActionResult> Get(long idNrcBend) =>
      Ok(await Mediator.Send(new Detail.Query { IdNrcBend = idNrcBend }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetNrcBend", new { idNrcBend = request.IdNrcBend }, request);
    }

    [HttpPut("{idNrcBend}")]
    public async Task<IActionResult> Update(int idNrcBend, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdNrcBend = idNrcBend });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{idNrcBend}")]
    public async Task<IActionResult> Delete(int idNrcBend) =>
      Ok(await Mediator.Send(new Delete.Command { IdNrcBend = idNrcBend }));
  }
}
