using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.DM.KabKotaCQ;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers.DM
{
  [Route("provinsi/{idProv}/kabkota")]
  public class KabKotaController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get(
      string idProv, [FromQuery] List.QueryDTO dto)
    {
      var query = dto.MapDTO(new List.Query {IdProv = idProv});

      return Ok(await Mediator.Send(query));
    }
  }
}