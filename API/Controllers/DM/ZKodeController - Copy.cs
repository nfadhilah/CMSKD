using Application.DM.JTrnlKasCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.DM
{
  public class JTrnlKasController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetJTrnlKas")]
    public async Task<IActionResult> Get(int id) =>
      Ok(await Mediator.Send(new Detail.Query { IdNoJeTra = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetJTrnlKas", new { id = request.IdNoJeTra }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdNoJeTra = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
      Ok(await Mediator.Send(new Delete.Command { IdNoJeTra = id }));
  }
}
