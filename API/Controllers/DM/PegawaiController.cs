using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DM.PegawaiCQ;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
  public class PegawaiController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) => Ok(await Mediator.Send(query));
  }
}