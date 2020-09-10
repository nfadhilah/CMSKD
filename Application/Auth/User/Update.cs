using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.BUD.BKUDCQ;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.BUD;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Auth.User
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long? IdKas { get; set; }
      public long IdSTS { get; set; }
      public long? IdBKas { get; set; }
      public long? IdUnit { get; set; }
      public DateTime? TglKas { get; set; }
      public DateTime? TglValid { get; set; }
      public string Uraian { get; set; }

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
        RuleFor(d => d.IdSTS).NotEmpty();
        RuleFor(d => d.IdKas).NotEmpty();
        RuleFor(d => d.IdBKas).NotEmpty();
      }
    }

    public class Command : BKUD, IRequest<BKUDDTO> { }

    public class Handler : IRequestHandler<Command, BKUDDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKUDDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.BKUD.FindByIdAsync(request.IdBKUD);

        if (updated == null)
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.BKUD.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.BKUD
          .FindAllAsync<DaftUnit, STS>(
            x => x.IdBKUD == updated.IdBKUD, x => x.Unit, x => x.STS);

        return _mapper.Map<BKUDDTO>(result.SingleOrDefault());
      }
    }
  }
}