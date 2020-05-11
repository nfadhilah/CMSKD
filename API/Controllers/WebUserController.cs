using Application.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class WebUserController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get() =>
      Ok(await Mediator.Send(new List.Query()));

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<IActionResult> Get(string id) =>
      Ok(await Mediator.Send(new Detail.Query { UserId = id }));
  }
}