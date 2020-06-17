using System.Threading.Tasks;
using Application.DM.MPgrmCQ;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
  public class MPgrmController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{idPrgrm}", Name = "GetMpgrm")]
    public async Task<IActionResult> Get(long idPrgrm) =>
      Ok(await Mediator.Send(new Detail.Query { IdPrgrm = idPrgrm }));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetMpgrm", new { idPrgrm = request.IdPrgrm },
        request);
    }

    [HttpPut("{idPrgrm}")]
    public async Task<IActionResult> Put(long idPrgrm, [FromBody] Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdPrgrm = idPrgrm });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{idPrgrm}")]
    public async Task<IActionResult> Delete(long idPrgrm) =>
      Ok(await Mediator.Send(new Delete.Command { IdPrgrm = idPrgrm }));
  }
}