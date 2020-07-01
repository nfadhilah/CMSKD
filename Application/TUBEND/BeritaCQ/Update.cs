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

namespace Application.TUBEND.BeritaCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public long IdKeg { get; set; }
      public string NoBerita { get; set; }
      public DateTime TglBA { get; set; }
      public long IdKontrak { get; set; }
      public string Urai_Berita { get; set; }
      public DateTime? TglValid { get; set; }
      public string KdStatus { get; set; }
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
        RuleFor(d => d.NoBerita).NotEmpty();
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.TglBA).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.IdKontrak).NotEmpty();
      }
    }

    public class Command : Berita, IRequest<BeritaDTO>
    {
    }

    public class Handler : IRequestHandler<Command, BeritaDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BeritaDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.Berita.FindByIdAsync(request.IdKontrak);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Berita.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.Berita
          .FindAllAsync<DaftUnit, MKegiatan, Kontrak>(
            x => x.IdBerita == updated.IdBerita, x => x.Unit,
            x => x.Kegiatan, x => x.Kontrak);

        return _mapper.Map<BeritaDTO>(result.SingleOrDefault());
      }
    }
  }
}