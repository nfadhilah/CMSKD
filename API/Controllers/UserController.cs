using Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class UserController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));


    [HttpGet("{id}", Name = "GetUser")]
    public async Task<IActionResult> Detail(int id) =>
      Ok(await Mediator.Send(new Detail.Query { UserId = id }));

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(Login.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpPost]
    public async Task<IActionResult> Register(Register.Command command)
    {
      var result = await Mediator.Send(command);

      return CreatedAtRoute("GetUser", new { id = result.Id }, result);
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
      Ok(await Mediator.Send(new Delete.Command { Id = id }));
  }
}