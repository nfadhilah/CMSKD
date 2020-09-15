using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.CommonDTO;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Dapper;
using Domain.PM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.PM.UserKegiatanCQ
{
  public class Create
  {
    public class DTO
    {
      public List<long> ListIdKeg { get; set; }
    }

    public class Command : IRequest<IEnumerable<UserKegiatanDTO>>
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

    public class
      Handler : IRequestHandler<Command, IEnumerable<UserKegiatanDTO>>
    {
      private readonly IDbContext _context;
      private readonly IUserAccessor _userAccessor;
      private readonly IMapper _mapper;

      public Handler(
        IDbContext context, IUserAccessor userAccessor, IMapper mapper)
      {
        _context = context;
        _userAccessor = userAccessor;
        _mapper = mapper;
      }

      public async Task<IEnumerable<UserKegiatanDTO>> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var models = request.ListIdKeg.Select(x => new UserKegiatan
        {
          UserId = request.UserId, IdKeg = x,
          AssignBy = _userAccessor.GetCurrentUsername(),
          AssignDate = DateTime.Now
        });

        var added = await _context.UserKegiatan.BulkInsertAsync(models);

        if (added < 0) throw new ApiException("Problem saving changes");

        var result = await _context.UserKegiatan.GetListUserKegiatan(
          request.UserId,
          request.ListIdKeg);

        return _mapper.Map<IEnumerable<UserKegiatanDTO>>(result);
      }
    }
  }
}