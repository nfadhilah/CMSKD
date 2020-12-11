using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Auth;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.WebUserCQ
{
  public class Create : IRequest<WebUserDTO>
  {

    public class Command : IRequest<WebUserDTO>
    {
      public string UserId { get; set; }
      public int KdTahap { get; set; }
      public string UnitKey { get; set; }
      public string NIP { get; set; }
      public string GroupId { get; set; }
      public string Pwd { get; set; }
      public string Nama { get; set; }
      public int? BlokId { get; set; }
      public int? TranSecure { get; set; }
      public int? StInsert { get; set; }
      public int? StUpdate { get; set; }
      public int? StDelete { get; set; }
      public string Ket { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.UserId).NotEmpty().MinimumLength(6);
        RuleFor(x => x.KdTahap).NotEmpty();
        RuleFor(x => x.UnitKey).NotEmpty();
        RuleFor(x => x.NIP).NotEmpty();
        RuleFor(x => x.GroupId).NotEmpty();
        RuleFor(x => x.Pwd).NotEmpty().MinimumLength(6);
        RuleFor(x => x.Nama).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, WebUserDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;
      private readonly IPasswordHasher _hasher;
      private readonly IEncryptionHelper _encryption;

      public Handler(IDbContext context, IMapper mapper, IPasswordHasher hasher, IEncryptionHelper encryption)
      {
        _context = context;
        _mapper = mapper;
        _hasher = hasher;
        _encryption = encryption;
      }

      public async Task<WebUserDTO> Handle(
      Command request, CancellationToken cancellationToken)
      {
        var existingUser =
          await _context.WebUser.FindAsync(x => x.UserId == request.UserId);

        if (existingUser != null) throw new ApiException("User sudah terpakai");

        var added = _mapper.Map<WebUser>(request);

        added.Pwd = _hasher.Create(request.Pwd);

        if (!await _context.WebUser.InsertAsync(added))
          throw new ApiException("Tambah data gagal");

        var result = await _context.WebUser.FindAsync<WebGroup>(x => x.UserId == added.UserId, x => x.WebGroup);

        return _mapper.Map<WebUserDTO>(result);
      }
    }

  }
}