using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.MappingKPA
{
  public class Create
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdPeg { get; set; }
      public string Jabatan { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt =>
        {
          opt.CreateMap<DTO, Command>();
        });

        _mapper = config.CreateMapper();
      }

      public Command MapDTO(Command destination) =>
        _mapper.Map(this, destination);
    }

    public class Validator : AbstractValidator<DTO>
    {
      public Validator()
      {
        RuleFor(x => x.IdPeg).NotEmpty();
        RuleFor(x => x.Jabatan).NotEmpty();
      }
    }

    public class Command : IRequest<KPA>
    {
      public long IdKPA { get; set; }
      public long IdUnit { get; set; }
      public long IdPeg { get; set; }
      public string Jabatan { get; set; }
    }

    public class Handler : IRequestHandler<Command, KPA>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<KPA> Handle(Command request, CancellationToken cancellationToken)
      {
        if (await _context.DaftUnit.FindByIdAsync(request.IdUnit) == null)
          throw new ApiException("Unit tidak ditemukan", (int)HttpStatusCode.NotFound);

        if (await _context.Pegawai.FindByIdAsync(request.IdPeg) == null)
          throw new ApiException("Pegawai tidak ditemukan", (int)HttpStatusCode.NotFound);

        var added = _mapper.Map<KPA>(request);

        if (!await _context.KPA.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}
