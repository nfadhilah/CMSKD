using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BUD.SP2DDetRPCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSP2DDetR { get; set; }
      public long IdPajak { get; set; }
      public decimal? Nilai { get; set; }
      public string Keterangan { get; set; }
      public string IdBilling { get; set; }
      public DateTime? TglBilling { get; set; }

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
        RuleFor(d => d.IdSP2DDetR).NotEmpty();
        RuleFor(d => d.IdPajak).NotEmpty();
      }
    }

    public class Command : SP2DDetRP, IRequest<SP2DDetRPDTO>
    {
    }

    public class Handler : IRequestHandler<Command, SP2DDetRPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<SP2DDetRPDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.SP2DDetRP.FindByIdAsync(request.IdSP2DDetRP);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.SP2DDetRP.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.SP2DDetRP
          .FindAllAsync<SP2DDetR, Pajak>(
            x => x.IdSP2DDetRP == updated.IdSP2DDetRP, x => x.SP2DDetR,
            x => x.Pajak);

        return _mapper.Map<SP2DDetRPDTO>(result.SingleOrDefault());
      }
    }
  }
}