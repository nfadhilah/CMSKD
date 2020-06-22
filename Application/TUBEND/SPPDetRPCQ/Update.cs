using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPDetRPCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public string NoSPP { get; set; }
      public string KdStatus { get; set; }
      public int KdBulan { get; set; }
      public long? IdBend { get; set; }
      public long IdSPD { get; set; }
      public long? IdPhk3 { get; set; }
      public int IdxKode { get; set; }
      public string NoReg { get; set; }
      public string KetOtor { get; set; }
      public string NoKontrak { get; set; }
      public string Keperluan { get; set; }
      public string Penolakan { get; set; }
      public DateTime? TglValid { get; set; }
      public DateTime? TglSPP { get; set; }
      public DateTime? Status { get; set; }

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
        RuleFor(d => d.NoSPP).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.KdBulan).NotEmpty();
        RuleFor(d => d.IdSPD).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Command : SPPDetRP, IRequest
    {
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Unit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SPPDetRP.FindByIdAsync(request.IdSPPDetRP);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SPPDetRP.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}