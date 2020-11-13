using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class CommonSpController : BaseController
  {
    [HttpPost]
    public async Task<IActionResult> ExecuteSp([FromBody] CommonSP.Command command)
      => Ok(await Mediator.Send(command));
  }
}