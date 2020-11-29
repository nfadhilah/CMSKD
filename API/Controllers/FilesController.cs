using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Base64Files;

namespace API.Controllers
{
  public class Base64FilesController : BaseController
  {
    [HttpPost("getfile")]
    public async Task<IActionResult> GetFile([FromBody] Query query) => Ok(await Mediator.Send(query));

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] Command command) => Ok(await Mediator.Send(command));
  }
}