using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Auth;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.User
{
  public class Register
  {
    public class Command : IRequest<WebUserDto>
    {
      public string UserId { get; set; }
      public long? IdUnit { get; set; }
      public int KdTahap { get; set; }
      public long? IdPeg { get; set; }
      public string Pwd { get; set; }
      public long GroupId { get; set; }
      public string Nama { get; set; }
      public string Email { get; set; }
      public int? BlokId { get; set; }
      public bool Transecure { get; set; }
      public bool StInsert { get; set; }
      public bool StUpdate { get; set; }
      public bool StDelete { get; set; }
      public string Ket { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.UserId).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Nama).NotEmpty();
        RuleFor(x => x.Pwd).NotEmpty().MinimumLength(6);
      }
    }

    public class Handler : IRequestHandler<Command, WebUserDto>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;
      private readonly IPasswordHasher _passwordHasher;

      public Handler(
        IDbContext context, IMapper mapper, IPasswordHasher passwordHasher)
      {
        _context = context;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
      }

      public async Task<WebUserDto> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var existingUser =
          await _context.WebUser.FindAsync(x => x.UserId == request.UserId);

        if (existingUser != null) throw new ApiException("User sudah terpakai");

        var model = _mapper.Map<WebUser>(request);

        model.Pwd = _passwordHasher.Create(request.Pwd);

        if (!await _context.WebUser.InsertAsync(model))
          throw new ApiException("Tambah data gagal");

        return _mapper.Map<WebUserDto>(model);
      }
    }
  }
}