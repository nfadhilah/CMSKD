using Application.DM.JTransCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.DM
{
  public class JTransController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "DetailJenisTransaksi")]
    public async Task<IActionResult> Get(string id) =>
      Ok(await Mediator.Send(new Detail.Query { IdTrans = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("DetailJenisTransaksi", new { id = request.IdTrans }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdTrans = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) =>
      Ok(await Mediator.Send(new Delete.Command { IdTrans = id }));
  }
}