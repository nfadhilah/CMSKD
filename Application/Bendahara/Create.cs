using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using MicroOrm.Dapper.Repositories.SqlGenerator.Filters;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bendahara
{
  public class Create
  {
    public class Command : IRequest<Bend>
    {
      // public string IdBend { get; set; }
    public string IdUnit { get; set; }
    public string IdPeg { get; set; }
    public string Jns_Bend { get; set; }
    public int StAktif { get; set; }
    public string IdBank { get; set; }
    public string Jab_Bend { get; set; }
    public string RekBend { get; set; }
    public string NPWPBend { get; set; }
    public Decimal? SaldoBend { get; set; }
    public Decimal? SaldoBendT { get; set; }
    public DateTime? TglStopBend { get; set; }
    public DateTime? DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.IdPeg).NotEmpty();
        RuleFor(d => d.Jns_Bend).NotEmpty();
        RuleFor(d => d.RekBend).NotEmpty();
        RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.Jab_Bend).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, Bend>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Bend> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Bend>(request);

        var lastObject = await _context.Bend
          .SetOrderBy(OrderInfo.SortDirection.DESC, d => d.IdBend).FindAsync();

        if (string.IsNullOrEmpty(lastObject.IdBend))
          added.IdBend = 1.ToString().PadLeft(10, '0');
        else
        {
          lastObject.IdBend = lastObject.IdBend.Replace("_", "");
          int.TryParse(lastObject.IdBend, out var id);
          added.IdBend = (id + 1).ToString() + "_";
        }

        if (!await _context.Bend.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}