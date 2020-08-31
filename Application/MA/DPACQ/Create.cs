using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPACQ
{
  public class Create
  {
    public class Command : IRequest<DPADTO>
    {
      public long IdUnit { get; set; }
      public string NoDPA { get; set; }
      public DateTime? TglDPA { get; set; }
      public string NoSah { get; set; }
      public string Keterangan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
      public string KdTahap { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.NoDPA).NotEmpty();
        RuleFor(d => d.KdTahap).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPA>(request);

        if (!await _context.DPA.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result =
          await _context.DPA.FindAllAsync<DaftUnit>(x => x.IdDPA == added.IdDPA,
            x => x.DaftUnit);

        return _mapper.Map<DPADTO>(result.Single());
      }
    }
  }
}