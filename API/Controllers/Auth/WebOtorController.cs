using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Auth.WebOtorCQ;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Auth
{
  public class WebOtorController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) => Ok(await Mediator.Send(query));

    [HttpGet("current")]
    public async Task<IActionResult> Current() => Ok(await Mediator.Send(new Current.Query()));
  }
}