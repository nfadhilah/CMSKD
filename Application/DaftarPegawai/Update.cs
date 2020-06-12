using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DaftarPegawai
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long NIP { get; set; }
      public long IdUnit { get; set; }
      public string KdGol { get; set; }
      public string Nama { get; set; }
      public string Alamat { get; set; }
      public string Jabatan { get; set; }
      public string PDDK { get; set; }
      public string NPWP { get; set; }
      public int StAktif { get; set; }
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
        RuleFor(d => d.NIP).NotEmpty();
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.KdGol).NotEmpty();
        RuleFor(d => d.Nama).NotEmpty();
        RuleFor(d => d.Alamat).NotEmpty();
        RuleFor(d => d.Jabatan).NotEmpty();
        RuleFor(d => d.PDDK).NotEmpty();
        RuleFor(d => d.NPWP).NotEmpty();
        RuleFor(d => d.StAktif).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
      }
    }
    
    public class Command : IRequest
    {
      public long IdPeg { get; set; }
      public long NIP { get; set; }
      public long IdUnit { get; set; }
      public string KdGol { get; set; }
      public string Nama { get; set; }
      public string Alamat { get; set; }
      public string Jabatan { get; set; }
      public string PDDK { get; set; }
      public string NPWP { get; set; }
      public int StAktif { get; set; }
      public DateTime? DateCreate { get; set; }
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

      public async Task<Unit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.Pegawai.FindAsync(x => x.IdPeg == request.IdPeg);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Pegawai.Update(updated))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}