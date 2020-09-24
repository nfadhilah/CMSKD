using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.PM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.PM.DiskusiPaketCQ
{
  public class Create
  {
    public class DTO
    {
      private readonly IMapper _mapper;

      public string Komentar { get; set; }
      public string Sender { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt => { opt.CreateMap<DTO, Command>(); });

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
        RuleFor(x => x.Komentar).NotEmpty();
        RuleFor(x => x.Sender).NotEmpty();
      }
    }

    public class Command : DiskusiPaket, IRequest<DiskusiPaket>
    {
    }

    public class
      Handler : IRequestHandler<Command, DiskusiPaket>
    {
      private readonly IDbContext _context;
      private readonly IUserAccessor _userAccessor;
      private readonly IMapper _mapper;

      public Handler(
        IDbContext context, IUserAccessor userAccessor, IMapper mapper)
      {
        _context = context;
        _userAccessor = userAccessor;
        _mapper = mapper;
      }

      public async Task<DiskusiPaket> Handle(
        Command request, CancellationToken cancellationToken)
      {
        if (!await _context.DiskusiPaket.InsertAsync(request))
          throw new ApiException("Problem saving changes");

        var result = await _context.DiskusiPaket
          .FindAsync(x => x.IdRUP == request.IdDiskusiPaket);

        return result;
      }
    }
  }
}