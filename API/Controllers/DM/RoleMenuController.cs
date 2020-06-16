using System.Threading.Tasks;
using Application.Auth.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
  [Route("role")]
  public class RoleMenuController : BaseController
  {
    [AllowAnonymous]
    [HttpGet("{roleId}/menu")]
    public async Task<IActionResult> Get(int roleId) =>
      Ok(await Mediator.Send(new List.Query { RoleId = roleId }));

    // [HttpGet("{roleId}/menu/{menuId}")]
  }
}