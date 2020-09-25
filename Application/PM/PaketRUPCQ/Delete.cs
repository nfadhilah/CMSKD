﻿using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using MediatR;
using Persistence;

namespace Application.PM.PaketRUPCQ
{
  public class Delete
  {
    public class Command : IRequest
    {
      public long IdRUP { get; set; }
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
        var deleted =
          await _context.PaketRup.FindByIdAsync(request.IdRUP);

        if (deleted == null)
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        if (deleted.FD.HasValue && deleted.FD.Value)
          throw  new ApiException("Paket tidak dapat dihapus karena sudah final");

        if (deleted.U.HasValue && deleted.U.Value)
          throw  new ApiException("Paket tidak dapat dihapus karena sudah diumumkan");

        if (!_context.PaketRup.Delete(deleted))
          throw new ApiException("Problem saving changes");

        return Unit.Value;
      }
    }
  }
}