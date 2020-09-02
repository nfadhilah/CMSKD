using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.TUBEND.SPMCQ;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using Domain.PM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.PM.PaketRUPCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public long IdKeg { get; set; }
      public decimal? NilaiPagu { get; set; }
      public DateTime? TglValid { get; set; }
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
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdJDana).NotEmpty();
        RuleFor(d => d.KodeRUP).NotEmpty();
      }
    }

    public class Command : PaketRUP, IRequest<PaketRUPDTO> { }

    public class Handler : IRequestHandler<Command, PaketRUPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<PaketRUPDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.PaketRup.FindByIdAsync(request.IdRUP);

        if (updated == null)
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.PaketRup.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.PaketRup
          .FindAllAsync<DaftUnit, MKegiatan, JPekerjaan, JDana, DaftPhk3>(
            x => x.IdRUP == updated.IdRUP,
            c => c.Unit,
            c => c.Keg, c => c.JnsPekerjaan, c => c.JDana, c => c.Phk3);

        return _mapper.Map<PaketRUPDTO>(result.SingleOrDefault());
      }
    }
  }
}