using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class FilesController : BaseController
  {
    [HttpPost("getfile")]
    public async Task<IActionResult> GetFile([FromBody] Query query) => Ok(await Mediator.Send(query));

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] Command command) => Ok(await Mediator.Send(command));
  }
}