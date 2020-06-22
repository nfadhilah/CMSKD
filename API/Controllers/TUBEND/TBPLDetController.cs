using Application.TUBEND.TBPLDetCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.TUBEND
{
  public class TBPLDetController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetTBPLDet")]
    public async Task<IActionResult> Get(long id) =>
      Ok(await Mediator.Send(new Detail.Query { IdTBPLDet = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetTBPLDet", new
      {
        id = request.IdTBPLDet
      }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdTBPLDet = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id) =>
      Ok(await Mediator.Send(new Delete.Command { IdTBPLDet = id }));
  }
}
