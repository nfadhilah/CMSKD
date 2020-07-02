using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPABlnRCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdDPAR { get; set; }
      public long IdBulan { get; set; }
      public Decimal? Nilai { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }

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
        RuleFor(d => d.IdDPAR).NotEmpty();
        RuleFor(d => d.IdBulan).NotEmpty();
      }
    }

    public class Command : DPABlnR, IRequest<DPABlnRDTO>
    {
    }

    public class Handler : IRequestHandler<Command, DPABlnRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPABlnRDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.DPABlnR.FindByIdAsync(request.IdDPABlnR);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DPABlnR.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await
          _context.DPABlnR.FindAllAsync<DPAR>(
            x => x.IdDPABlnR == updated.IdDPABlnR, x => x.DPAR);

        return _mapper.Map<DPABlnRDTO>(result.Single());
      }
    }
  }
}