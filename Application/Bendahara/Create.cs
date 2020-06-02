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
      // public string KeyBend { get; set; }
    public string UnitKey { get; set; }
    public string NIP { get; set; }
    public string Jns_Bend { get; set; }
    public int StAktif { get; set; }
    public string KdBank { get; set; }
    public string Jab_Bend { get; set; }
    public string RekBend { get; set; }
    public string NPWPBend { get; set; }
    public Decimal? SaldoBend { get; set; }
    public Decimal? SaldoBendT { get; set; }
    public DateTime? TglStopBend { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.KeyBend).NotEmpty();
        RuleFor(d => d.NIP).NotEmpty();
        RuleFor(d => d.Jns_Bend).NotEmpty();
        RuleFor(d => d.RekBend).NotEmpty();
        RuleFor(d => d.KdBank).NotEmpty();
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
          .SetOrderBy(OrderInfo.SortDirection.DESC, d => d.KeyBend).FindAsync();

        if (string.IsNullOrEmpty(lastObject.KeyBend))
          added.KeyBend = 1.ToString().PadLeft(10, '0');
        else
        {
          lastObject.KeyBend = lastObject.KeyBend.Replace("_", "");
          int.TryParse(lastObject.KeyBend, out var id);
          added.KeyBend = (id + 1).ToString() + "_";
        }

        if (!await _context.Bend.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}