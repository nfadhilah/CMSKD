using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.DM.KelurahanCQ;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers.DM
{
  [Route("kecamatan/{idKec}/kelurahan")]
  public class KelurahanController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get(
      string idKec, [FromQuery] List.QueryDTO dto)
    {
      var query = dto.MapDTO(new List.Query {IdKec = idKec});

      return Ok(await Mediator.Send(query));
    }
  }
}