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

namespace Application.DM.DaftBankCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string AkBank { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string Cabang { get; set; }
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
        RuleFor(d => d.AkBank).NotEmpty();
        RuleFor(d => d.Alamat).NotEmpty();
        RuleFor(d => d.Telepon).NotEmpty();
        RuleFor(d => d.Cabang).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
      }
    }

    public class Command : DaftBank, IRequest<DaftBankDTO>
    {
    }

    public class Handler : IRequestHandler<Command, DaftBankDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftBankDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.DaftBank.FindAsync(x => x.KdBank == request.KdBank);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DaftBank.Update(updated))
          throw new ApiException("Problem saving changes");

        return _mapper.Map<DaftBankDTO>(updated);
      }
    }
  }
}