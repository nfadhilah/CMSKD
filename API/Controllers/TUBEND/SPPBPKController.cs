using Application.TUBEND.SPPBPKCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.TUBEND
{
  public class SPPBPKController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetSPPBPK")]
    public async Task<IActionResult> Get(long id) =>
      Ok(await Mediator.Send(new Detail.Query { IdSPPBPK = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetSPPBPK", new { id = request.IdSPPBPK }, request);
    }

    /// <summary>
    /// Bulk Insert ke Table SPPBPK sekaligus bulk insert ke Table SPPDETR
    /// </summary>
    /// <param name="idSPP">IdSPP</param>
    /// <param name="dto">idBPKList diisi dengan list dari IdBPK eg: [1, 2, 3, 4]</param>
    /// <returns></returns>
    [HttpPost("SPP/{idSPP}/bulk")]
    public async Task<IActionResult> BulkInsert(long idSPP, BulkInsert.DTO dto)
    {
      var command = dto.MapDTO(new BulkInsert.Command { IdSPP = idSPP });
      return Ok(await Mediator.Send(command));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdSPPBPK = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id) =>
      Ok(await Mediator.Send(new Delete.Command { IdSPPBPK = id }));
  }
}