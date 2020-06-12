using Application.Kegiatan;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class MKegiatanController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{idKeg}", Name = "GetMKegiatan")]
    public async Task<IActionResult> Get(long idKeg) =>
      Ok(await Mediator.Send(new Detail.Query { IdKeg = idKeg }));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetMKegiatan", new { idKeg = request.IdKeg },
        request);
    }

    [HttpPut("{idKeg}")]
    public async Task<IActionResult> Put(long idKeg, [FromBody] Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdKeg = idKeg });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{idKeg}")]
    public async Task<IActionResult> Delete(long idKeg) =>
      Ok(await Mediator.Send(new Delete.Command { IdKeg = idKeg }));
  }
}
