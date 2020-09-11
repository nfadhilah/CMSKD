using Application.Auth.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Auth
{
  public class WebUserController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));


    [HttpGet("{id}", Name = "GetUser")]
    public async Task<IActionResult> Detail(string id) =>
      Ok(await Mediator.Send(new Detail.Query {UserId = id}));

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(Login.Command command) =>
      Ok(await Mediator.Send(command));

    [HttpGet("current")]
    public async Task<IActionResult> CurrentUser() =>
      Ok(await Mediator.Send(new CurrentUser.Query()));

    [HttpPost]
    public async Task<IActionResult> Register(Register.Command command)
    {
      var result = await Mediator.Send(command);

      return CreatedAtRoute("GetUser", new {id = result.UserId}, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command {UserId = id});
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) =>
      Ok(await Mediator.Send(new Delete.Command {UserId = id}));
  }
}