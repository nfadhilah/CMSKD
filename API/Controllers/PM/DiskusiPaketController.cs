using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.PM.DiskusiPaketCQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PM
{
  [Route("api/paket-rup/{idRUP}/diskusi")]
  public class DiskusiPaketController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get(long idRUP, [FromQuery] List.QueryDTO queryDto)
    {
      var query = queryDto.MapDTO(new List.Query { IdRUP = idRUP });
      return Ok(await Mediator.Send(query));
    }
      
    [HttpPost]
    public async Task<IActionResult> Post(long idRUP, [FromBody] Create.DTO dto)
    {
      var command = dto.MapDTO(new Create.Command{IdRUP = idRUP});
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{idDiskusi}")]
    public async Task<IActionResult> Post(long idRup, long idDiskusi)
    {
      var command = new Delete.Command {IdDiskusiPaket = idDiskusi, };
      return Ok(await Mediator.Send(command));
    }
  }
}