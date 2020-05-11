using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rekanan
{
  public class Create
  {
    public class Command : IRequest<DaftPhk3>
    {
      // public string Kdp3 { get; set; }
      public string NmP3 { get; set; }
      public string NmInst { get; set; }
      public string NoRcP3 { get; set; }
      public string NmBank { get; set; }
      public string JnsUsaha { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string NPWP { get; set; }
      public string UnitKey { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.Kdp3).NotEmpty();
        RuleFor(d => d.NmP3).NotEmpty();
        RuleFor(d => d.NmInst).NotEmpty();
        RuleFor(d => d.NoRcP3).NotEmpty();
        RuleFor(d => d.NmBank).NotEmpty();
        RuleFor(d => d.NPWP).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DaftPhk3>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftPhk3> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DaftPhk3>(request);

        var lastObject = await _context.DaftPhk3
          .SetOrderBy(OrderInfo.SortDirection.DESC, d => d.KdP3).FindAsync();

        if (string.IsNullOrEmpty(lastObject.KdP3))
          added.KdP3 = 1.ToString().PadLeft(10, '0');
        else
        {
          int.TryParse(lastObject.KdP3, out var id);
          added.KdP3 = (id + 1).ToString().PadLeft(10, '0');
        }

        if (!await _context.DaftPhk3.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}