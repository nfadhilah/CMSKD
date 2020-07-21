using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BkBankCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public long IdBend { get; set; }
      public string NoBuku { get; set; }
      public string KdStatus { get; set; }
      public DateTime? TglBuku { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }

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
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.NoBuku).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
      }
    }

    public class Command : BkBank, IRequest<BKBankDTO>
    {
    }

    public class Handler : IRequestHandler<Command, BKBankDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKBankDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.BkBank.FindByIdAsync(request.IdBkBank);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.BkBank.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = (await _context.BkBank
          .FindAllAsync<DaftUnit, Bend, StatTrs>(
            x => x.IdBkBank == updated.IdBkBank, x => x.Unit,
            x => x.Bend, x => x.StatTrs)).SingleOrDefault();

        return _mapper.Map<BKBankDTO>(result);
      }
    }
  }
}