﻿using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BPKDetRPCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdBPKDetR { get; set; }
      public long IdPajak { get; set; }
      public decimal? Nilai { get; set; }

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
        RuleFor(d => d.IdBPKDetR).NotEmpty();
        RuleFor(d => d.IdPajak).NotEmpty();
      }
    }

    public class Command : BPKDetRP, IRequest<BPKDetRPDTO>
    {
    }

    public class Handler : IRequestHandler<Command, BPKDetRPDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BPKDetRPDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.BPKDetRP.FindByIdAsync(request.IdBPKDetRP);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.BPKDetRP.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.BPKDetRP
          .FindAllAsync<BPKDetR, Pajak>(
            x => x.IdBPKDetRP == updated.IdBPKDetRP, x => x.BPKDetR,
            x => x.Pajak);

        return _mapper.Map<BPKDetRPDTO>(result.SingleOrDefault());
      }
    }
  }
}