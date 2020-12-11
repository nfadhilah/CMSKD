using Application.Common.DTOS;
using Dapper;
using FluentValidation;
using MediatR;
using Persistence;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common
{
  public class CommonSP
  {
    public class Command : CommonSpParams, IRequest<object>
    {
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.SpName).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, object>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<object> Handle(Command request, CancellationToken cancellationToken)
      {
        var parameters = new DynamicParameters();


        if (request.Parameters != null)
        {
          foreach (var (key, value) in request.Parameters)
            parameters.Add(key, value);
        }


        return await _context.Connection.QueryAsync(request.SpName,
          parameters, commandType: CommandType.StoredProcedure);
      }
    }
  }
}