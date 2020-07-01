using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BeritaDetRCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdBerita { get; set; }
      public long IdRek { get; set; }
      public decimal? Nilai { get; set; }
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
        RuleFor(d => d.IdBerita).NotEmpty();
        RuleFor(d => d.IdRek).NotEmpty();
      }
    }

    public class Command : BeritaDetR, IRequest<BeritaDetRDTO>
    {
    }

    public class Handler : IRequestHandler<Command, BeritaDetRDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BeritaDetRDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.BeritaDetR.FindByIdAsync(request.IdBeritaDet);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.BeritaDetR.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.BeritaDetR
          .FindAllAsync<Berita, DaftRekening>(
            x => x.IdBeritaDet == updated.IdBeritaDet, x => x.Berita,
            x => x.Rekening);

        return _mapper.Map<BeritaDetRDTO>(result);
      }
    }
  }
}