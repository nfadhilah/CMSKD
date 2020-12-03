﻿using Application.DM.DocMetaCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.DM
{
  public class DocMetaController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) => Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetDocMeta")]
    public async Task<IActionResult> Get(int id) => Ok(await Mediator.Send(new Detail.Query { Id = id }));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Create.Command command)
    {
      var result = await Mediator.Send(command);

      return CreatedAtRoute("GetDocMeta", new { result.Id }, result);
    }
  }
}
