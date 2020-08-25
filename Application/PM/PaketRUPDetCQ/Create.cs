using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.PM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.PM.PaketRUPDetCQ
{
  public class Create
  {
    public class Command : IRequest<PaketRUPDetDTO>
    {
      public long IdRUP { get; set; }
      public string KodeRUP { get; set; }
      public string NmPaket { get; set; }
      public string Lokasi { get; set; }
      public string Volume { get; set; }
      public string UraiPaket { get; set; }
      public long? IdJnsPekerjaan { get; set; }
      public DateTime? AwalPekerjaan { get; set; }
      public DateTime? AkhirPekerjaan { get; set; }
      public long IdJDana { get; set; }
      public long IdPhk3 { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdRUP).NotEmpty();
        RuleFor(d => d.KodeRUP).NotEmpty();
        RuleFor(d => d.NmPaket).NotEmpty();
        RuleFor(d => d.Lokasi).NotEmpty();
        RuleFor(d => d.Volume).NotEmpty();
        RuleFor(d => d.UraiPaket).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
        RuleFor(d => d.IdPhk3).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, PaketRUPDetDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PaketRUPDetDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<PaketRUPDet>(request);

        if (!await _context.PaketRupDet.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.PaketRupDet
          .FindAllAsync<JPekerjaan, JDana, DaftPhk3>(
            x => x.IdRUPDet == added.IdRUPDet,
            c => c.JnsPekerjaan,
            c => c.JDana, c => c.Phk3);

        return _mapper.Map<PaketRUPDetDTO>(result.SingleOrDefault());
      }
    }
  }
}