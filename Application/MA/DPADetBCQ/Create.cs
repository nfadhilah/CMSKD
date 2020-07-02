using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPADetBCQ
{
  public class Create
  {
    public class Command : IRequest<DPADetBDTO>
    {
      public long IdDPAB { get; set; }
      public string KdNilai { get; set; }
      public string KdJabar { get; set; }
      public string Uraian { get; set; }
      public decimal? JumBYek { get; set; }
      public string Satuan { get; set; }
      public decimal? Tarif { get; set; }
      public decimal? SubTotal { get; set; }
      public string Ekspresi { get; set; }
      public byte InclSubtotal { get; set; }
      public string Type { get; set; }
      public string IdStdHarga { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdDPAB).NotEmpty();
        RuleFor(d => d.KdNilai).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DPADetBDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADetBDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DPADetB>(request);

        if (!await _context.DPADetB.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return _mapper.Map<DPADetBDTO>(added);
      }
    }
  }
}