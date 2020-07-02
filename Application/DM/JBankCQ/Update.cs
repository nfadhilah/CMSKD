using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.JBankCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string KdBank { get; set; }
      public string NmBank { get; set; }
      public string Uraian { get; set; }
      public string Akronim { get; set; }
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
        RuleFor(d => d.KdBank).NotEmpty();
        RuleFor(d => d.NmBank).NotEmpty();
        RuleFor(d => d.Uraian).NotEmpty();
        RuleFor(d => d.Akronim).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
      }
    }

    public class Command : JBank, IRequest<JBank>
    {
    }

    public class Handler : IRequestHandler<Command, JBank>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBank> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.JBank.FindByIdAsync(request.IdBank);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.JBank.Update(updated))
          throw new ApiException("Problem saving changes");

        return updated;
      }
    }
  }
}