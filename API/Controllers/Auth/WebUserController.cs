﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Auth.WebUserCQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Auth
{
  public class WebUserController : BaseController
  {
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(Login.Command command) =>
      Ok(await Mediator.Send(command));

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) => Ok(await Mediator.Send(query));
  }
}