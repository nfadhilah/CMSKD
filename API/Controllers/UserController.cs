using Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class UserController : BaseController
  {
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(Login.Query query) =>
      Ok(await Mediator.Send(query));
  }
}