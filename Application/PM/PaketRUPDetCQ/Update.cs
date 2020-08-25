using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.PM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.PM.PaketRUPDetCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

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

      public DTO()
      {
        var config = new MapperConfiguration(opt =>
        {
          opt.CreateMap<DTO, Command>();
        });

        _mapper = config.CreateMapper();
      }

      public Command MapDTO(Command destination)
      {
        return _mapper.Map(this, destination);
      }
    }

    public class Validator : AbstractValidator<DTO>
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

    public class Command : PaketRUPDet, IRequest<PaketRUPDetDTO> { }

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
        var updated =
          await _context.PaketRupDet.FindByIdAsync(request.IdRUPDet);

        if (updated == null)
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.PaketRupDet.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.PaketRupDet
          .FindAllAsync<JPekerjaan, JDana, DaftPhk3>(
            x => x.IdRUPDet == updated.IdRUPDet,
            c => c.JnsPekerjaan,
            c => c.JDana, c => c.Phk3);

        return _mapper.Map<PaketRUPDetDTO>(result.SingleOrDefault());
      }
    }
  }
}