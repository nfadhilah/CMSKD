using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BaseController : ControllerBase
  {
    private IMediator _mediator;
    private IDbContext _dbContext;

    protected IMediator Mediator => _mediator ??=
      HttpContext.RequestServices.GetService<IMediator>();

    protected IDbContext DbContext => _dbContext ??=
      HttpContext.RequestServices.GetService<IDbContext>();
  }
}