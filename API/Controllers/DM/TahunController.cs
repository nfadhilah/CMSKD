using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DM;
using Application.DM.TahunCQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
  public class TahunController : BaseController
  {
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) => Ok(await Mediator.Send(query));
  }
}