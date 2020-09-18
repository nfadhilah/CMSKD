using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.DM.KecamatanCQ;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers.DM
{
  [Route("kabkota/{idKabKota}/kecamatan")]
  public class KecamatanController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get(
      string idKabKota, [FromQuery] List.QueryDTO dto)
    {
      var query = dto.MapDTO(new List.Query {IdKabKota = idKabKota});

      return Ok(await Mediator.Send(query));
    }
  }
}