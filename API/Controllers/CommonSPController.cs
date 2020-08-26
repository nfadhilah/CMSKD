using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class CommonSpController : BaseController
  {
    [HttpPost]
    public async Task<IActionResult> Get(CommonSpParams dto)
    {
      var parameters = new DynamicParameters();

      if (dto.Parameters == null && !(dto.Parameters?.Count > 0))
        return Ok(await DbContext.Connection.QueryAsync(dto.SpName,
          parameters, commandType: CommandType.StoredProcedure));

      foreach (var (key, value) in dto.Parameters)
        parameters.Add(key, value);

      return Ok(await DbContext.Connection.QueryAsync(dto.SpName,
        parameters, commandType: CommandType.StoredProcedure));
    }
  }
}