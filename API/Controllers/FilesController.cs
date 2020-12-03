using Application.Common.Files;
using AutoWrapper.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class FilesController : BaseController
  {
    [HttpGet]
    [AutoWrapIgnore]
    public async Task<IActionResult> Get([FromQuery] Download.Query query) => File(await Mediator.Send(query), query.FileType);


    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] Upload.Command command) => Ok(await Mediator.Send(command));
  }
}