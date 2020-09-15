﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.PM.UserKegiatanCQ
{
  public class Delete
  {
    public class DTO
    {
      public List<long> ListIdKeg { get; set; }
    }

    public class Command : IRequest
    {
      public string UserId { get; set; }
      public List<long> ListIdKeg { get; set; }
    }

    public class Validator : AbstractValidator<DTO>
    {
      public Validator()
      {
        RuleFor(x => x.ListIdKeg).NotEmpty();
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

      public async Task<Unit> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var deleted = await _context.UserKegiatan.GetListUserKegiatan(
          request.UserId,
          request.ListIdKeg);

        if (!deleted.Any())
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        await _context.UserKegiatan.DeleteByUserIdAndListIdKeg(request.UserId,
          request.ListIdKeg);

        return Unit.Value;
      }
    }
  }
}