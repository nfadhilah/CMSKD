using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.PM.UserKegiatanCQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PM
{
  public class KegiatanUserController : BaseController
  {
    [AllowAnonymous]
    [HttpGet("tree")]
    public async Task<IActionResult> Get([FromQuery] Tree.Query query) =>
      Ok(await Mediator.Send(query));
  }
}