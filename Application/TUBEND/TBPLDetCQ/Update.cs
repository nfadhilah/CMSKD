using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TBPLDetCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdTBPL { get; set; }
      public long IdBend { get; set; }
      public int IdNoJeTra { get; set; }
      public decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }

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
        RuleFor(d => d.IdTBPL).NotEmpty();
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.IdNoJeTra).NotEmpty();
      }
    }

    public class Command : TBPLDet, IRequest<TBPLDetDTO>
    {
    }

    public class Handler : IRequestHandler<Command, TBPLDetDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TBPLDetDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.TBPLDet.FindByIdAsync(request.IdTBPL);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.TBPLDet.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.TBPLDet
          .FindAllAsync<TBPL, Bend, JTrnlKas>(
            x => x.IdTBPLDet == updated.IdTBPLDet, x => x.TBPL,
            x => x.Bend, x => x.JTrnlKas);

        return _mapper.Map<TBPLDetDTO>(result);
      }
    }
  }
}