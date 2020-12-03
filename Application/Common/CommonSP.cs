﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOS;
using Dapper;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Common
{
  public class CommonSP
  {
    public class Command : CommonSpParams, IRequest<object>
    {
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.SpName).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, object>
    {
      private readonly IDbContext _context;

      public Handler(IDbContext context)
      {
        _context = context;
      }

      public async Task<object> Handle(Command request, CancellationToken cancellationToken)
      {
        var parameters = new DynamicParameters();

        foreach (var (key, value) in request.Parameters)
          parameters.Add(key, value);

        return await _context.Connection.QueryAsync(request.SpName,
          parameters, commandType: CommandType.StoredProcedure);
      }
    }
  }
}