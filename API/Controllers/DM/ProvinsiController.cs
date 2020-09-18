using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.DM.ProvinsiCQ;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers.DM
{
  public class ProvinsiController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));
  }
}