using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DM.KPACQ
{
  public class Create
  {
    public class Command : IRequest<KPADTO>
    {
      public long IdPeg { get; set; }
      public string Jabatan { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.IdPeg).NotEmpty();
        RuleFor(x => x.Jabatan).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, KPADTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<KPADTO> Handle(Command request, CancellationToken cancellationToken)
      {
        if (await _context.Pegawai.FindByIdAsync(request.IdPeg) == null)
          throw new ApiException("Pegawai tidak ditemukan", (int)HttpStatusCode.NotFound);

        var added = _mapper.Map<KPA>(request);

        if (!await _context.KPA.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        var result = await _context.KPA
          .FindAllAsync<Pegawai>(x => x.IdKPA == added.IdKPA, x => x.Pegawai);

        return _mapper.Map<KPADTO>(result.SingleOrDefault());
      }
    }
  }
}
