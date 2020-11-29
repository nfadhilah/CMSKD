using Application.Common.Files;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class FilesController : BaseController
  {

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] Command command) => Ok(await Mediator.Send(command));
  }
}