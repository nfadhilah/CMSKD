using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.MA;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MA.DPACQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public string NoDPA { get; set; }
      public DateTime? TglDPA { get; set; }
      public string NoSah { get; set; }
      public string Keterangan { get; set; }
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
        RuleFor(d => d.NoDPA).NotEmpty();
      }
    }

    public class Command : DPA, IRequest<DPADTO>
    {
    }

    public class Handler : IRequestHandler<Command, DPADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DPADTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.DPA.FindByIdAsync(request.IdDPA);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DPA.Update(updated))
          throw new ApiException("Problem saving changes");

        var result =
          await _context.DPA.FindAllAsync<DaftUnit>(x => x.IdDPA == updated.IdDPA,
            x => x.DaftUnit);

        return _mapper.Map<DPADTO>(result.Single());
      }
    }
  }
}