using Application.TUBEND.SPPCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Create = Application.TUBEND.SPMCQ.Create;
using Delete = Application.TUBEND.SPMCQ.Delete;
using Detail = Application.TUBEND.SPMCQ.Detail;
using List = Application.TUBEND.SPMCQ.List;
using Update = Application.TUBEND.SPMCQ.Update;

namespace API.Controllers.TUBEND
{
  public class SPMController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("lastDocNo")]
    public async Task<IActionResult> Get([FromQuery] GetLastDocNumber.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("lastRegNo")]
    public async Task<IActionResult> Get([FromQuery] GetLastRegNumber.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetSPM")]
    public async Task<IActionResult> Get(long id) =>
      Ok(await Mediator.Send(new Detail.Query { IdSPM = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetSPM", new { id = request.IdSPM }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdSPM = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id) =>
      Ok(await Mediator.Send(new Delete.Command { IdSPM = id }));
  }
}