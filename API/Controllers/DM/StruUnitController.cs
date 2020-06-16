using System.Threading.Tasks;
using Application.DM.StrukturUnit;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DM
{
    public class StruUnitController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] List.Query query) =>
          Ok(await Mediator.Send(query));

        [HttpGet("{id}", Name = "GetKodeStruUnit")]
        public async Task<IActionResult> Get(long id) =>
          Ok(await Mediator.Send(new Detail.Query { IdStruUnit = id }));

        [HttpPost]
        public async Task<IActionResult> Create(Create.Command command)
        {
            var request = await Mediator.Send(command);
            return CreatedAtRoute("GetKodeStruUnit", new { id = request.IdStruUnit }, request);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Update.Command command)
        {
            command.IdStruUnit = id;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) =>
          Ok(await Mediator.Send(new Delete.Command { IdStruUnit = id }));
    }
}
