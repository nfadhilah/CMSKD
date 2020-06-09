﻿using Application.Rekanan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  [AllowAnonymous]
  public class DaftPhk3Controller : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetDafPhk3")]
    public async Task<IActionResult> Get(int id) =>
      Ok(await Mediator.Send(new Detail.Query { IdPhk3 = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetDaftPhk3", new { id = request.IdPhk3 }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command());
      command.IdPhk3 = id;
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
      Ok(await Mediator.Send(new Delete.Command { IdPhk3 = id }));
  }
}