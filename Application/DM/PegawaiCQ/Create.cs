using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.PegawaiCQ
{
  public class Create
  {
    public class Command : IRequest<Pegawai>
    {
      // public long IdPeg { get; set; }
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

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdPeg).NotEmpty();
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

    public class Handler : IRequestHandler<Command, Pegawai>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Pegawai> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<Pegawai>(request);

        if (!await _context.Pegawai.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}