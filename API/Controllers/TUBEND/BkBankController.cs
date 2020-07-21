﻿using Application.TUBEND.BkBankCQ;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.TUBEND
{
  public class BkBankController : BaseController
  {
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] List.Query query) =>
      Ok(await Mediator.Send(query));

    [HttpGet("{id}", Name = "GetBkBank")]
    public async Task<IActionResult> Get(long id) =>
      Ok(await Mediator.Send(new Detail.Query { IdBkBank = id }));

    [HttpPost]
    public async Task<IActionResult> Create(Create.Command command)
    {
      var request = await Mediator.Send(command);
      return CreatedAtRoute("GetBkBank", new { id = request.IdBkBank }, request);
    }

    /// <summary>
    /// Post BKBANK dan BKBANKDET
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("header-detail")]
    public async Task<IActionResult> CreateHeaderDetail(
      CreateHeaderDetail.Command command) => Ok(await Mediator.Send(command));


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Update.DTO dto)
    {
      var command = dto.MapDTO(new Update.Command { IdBkBank = id });
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id) =>
      Ok(await Mediator.Send(new Delete.Command { IdBkBank = id }));

    /// <summary>
    /// Delete BKBANK dan BKBANKDET
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}/header-detail")]
    public async Task<IActionResult> DeleteHeaderDetail(long id) =>
      Ok(await Mediator.Send(new DeleteHeaderDetail.Command { IdBkBank = id }));
  }
}