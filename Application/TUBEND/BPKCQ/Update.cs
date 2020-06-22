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

namespace Application.TUBEND.BPKCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public long IdPhk3 { get; set; }
      public string NoBPK { get; set; }
      public string KdStatus { get; set; }
      public string JBayar { get; set; }
      public int IdxKode { get; set; }
      public long IdBend { get; set; }
      public DateTime? TglBPK { get; set; }
      public string UraiBPK { get; set; }
      public DateTime? TglValid { get; set; }
      public long? IdBerita { get; set; }
      public long? KdRilis { get; set; }
      public int? StKirim { get; set; }
      public int? StCair { get; set; }
      public string NoRef { get; set; }

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
        RuleFor(d => d.NoBPK).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
        RuleFor(d => d.IdxKode).NotEmpty();
      }
    }

    public class Command : BPK, IRequest
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
          await _context.BPK.FindByIdAsync(request.IdBPK);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.BPK.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}