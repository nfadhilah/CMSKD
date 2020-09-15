using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.PM.UserKegiatanCQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PM
{
  public class UserKegiatanController : BaseController
  {
    [HttpGet("{userId}/kegiatan")]
    public async Task<IActionResult> Get(string userId, [FromQuery] List.QueryDTO queryDto)
    {
      var query = queryDto.MapDTO(new List.Query { UserId = userId });
      return Ok(await Mediator.Send(query));
    }
      
    [HttpPost("{userId}/kegiatan")]
    public async Task<IActionResult> Post(string userId, [FromBody] Create.DTO dto)
    {
      var command = new Create.Command { UserId = userId, ListIdKeg = dto.ListIdKeg};
      return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{userId}/kegiatan")]
    public async Task<IActionResult> Delete(string userId, [FromBody] Delete.DTO dto)
    {
      var command = new Delete.Command { UserId = userId, ListIdKeg = dto.ListIdKeg };
      return Ok(await Mediator.Send(command));
    }
  }
}