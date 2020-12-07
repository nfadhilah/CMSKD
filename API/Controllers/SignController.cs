using Application.Common.Files;
using AutoWrapper.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class SignController : BaseController
  {
    [HttpPost]
    [AutoWrapIgnore]
    public async Task<IActionResult> Sign(Sign.Command command)
    {
      const string contentType = "application/pdf";

      var stream = await Mediator.Send(command);

      return File(stream, contentType);
    }
  }
}
