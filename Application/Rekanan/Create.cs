using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Rekanan
{
  public class Create
  {
    public class Command : IRequest<DaftPhk3>
    {
      public string NmPhk3 { get; set; }
      public string NmInst { get; set; }
      public int IdBank { get; set; }
      public string CabangBank { get; set; }
      public string AlamatBank { get; set; }
      public string NoRekBank { get; set; }
      public int IdJUsaha { get; set; }
      public string Alamat { get; set; }
      public string Telepon { get; set; }
      public string NPWP { get; set; }
      public int StValid { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.NmPhk3).NotEmpty();
        RuleFor(d => d.NmInst).NotEmpty();
        RuleFor(d => d.NoRekBank).NotEmpty();
        RuleFor(d => d.IdBank).NotEmpty();
        RuleFor(d => d.NPWP).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, DaftPhk3>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<DaftPhk3> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<DaftPhk3>(request);

        if (!await _context.DaftPhk3.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}