using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DM.MetodePengadaanCQ;

namespace API.Controllers.DM
{
  public class MetodePengadaanController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetMetodePengadaan")]
    public async Task<IActionResult> Get(long id) =>
      Ok(await Mediator.Send(new Detail.Query {IdMetodePengadaan = id}));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetMetodePengadaan",
        new {id = request.IdMetodePengadaan}, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command {IdMetodePengadaan = id});
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id) =>
      Ok(await Mediator.Send(new Delete.Command {IdMetodePengadaan = id}));
  }
}