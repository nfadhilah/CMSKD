﻿using Application.DM.PajakCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.DM
{
  public class PajakController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "DetailPajak")]
    public async Task<IActionResult> Get(long id) =>
      Ok(await Mediator.Send(new Detail.Query { IdPajak = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("DetailPajak", new { id = request.IdPajak }, request);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdPajak = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id) =>
      Ok(await Mediator.Send(new Delete.Command { IdPajak = id }));
  }
}