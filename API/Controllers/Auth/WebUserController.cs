using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Auth.WebUserCQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Auth
{
  public class WebUserController : BaseController
  {
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(Login.Command command) =>
      Ok(await Mediator.Send(command));

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) => Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetWebUser")]
    public async Task<IActionResult> Get(string id) => Ok(await Mediator.Send(new Detail.Query {UserId = id}));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Create.Command command)
    {
      var result = await Mediator.Send(command);

      return CreatedAtRoute("GetWebUser", new {id = result.UserId}, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command {UserId = id});

      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) => Ok(await Mediator.Send(new Delete.Command {UserId = id}));
  }
}