using Application.TUBEND.SPPCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.TUBEND
{
  public class SPPController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("group-by-subkeg")]
    public async Task<IActionResult> Get([FromQuery] ListGroupBySubKeg.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("lastDocNo")]
    public async Task<IActionResult> Get([FromQuery] GetLastDocNumber.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("lastRegNo")]
    public async Task<IActionResult> Get(
      [FromQuery] GetLastRegNumber.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetSPP")]
    public async Task<IActionResult> Get(long id) =>
      Ok(await Mediator.Send(new Detail.Query { IdSPP = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetSPP", new { id = request.IdSPP }, request);
    }

    [HttpPost("ls")]
    public async Task<IActionResult> CreateSPPLS(CreateSPPLS.Command command) =>
      Ok(await Mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdSPP = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpPut("{id}/ls")]
    public async Task<IActionResult> UpdateSPPLS(long id, UpdateSPPLS.DTO dto)
    {
      var command = dto.MapDTO(new UpdateSPPLS.Command { IdSPP = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id) =>
      Ok(await Mediator.Send(new Delete.Command { IdSPP = id }));

    [HttpDelete("{id}/ls")]
    public async Task<IActionResult> DeleteSPPLS(long id) =>
      Ok(await Mediator.Send(new DeleteSPPLS.Command { IdSPP = id }));
  }
}