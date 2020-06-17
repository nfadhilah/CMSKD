using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.DaftUnitCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public string KdUnit { get; set; }
      public string NmUnit { get; set; }
      public int KdLevel { get; set; }
      public string Type { get; set; }
      public string AkroUnit { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public int StAktif { get; set; }
      public DateTime DateCreate { get; set; }

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

    public class Command : IRequest
    {
      public int IdUnit { get; set; }
      public string KdUnit { get; set; }
      public string NmUnit { get; set; }
      public int KdLevel { get; set; }
      public string Type { get; set; }
      public string AkroUnit { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public int StAktif { get; set; }
      public DateTime DateCreate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
      }
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

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.DaftUnit.FindByIdAsync(request.IdUnit);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.DaftUnit.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}
