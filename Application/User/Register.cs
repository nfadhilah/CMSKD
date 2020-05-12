using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User
{
  public class Register
  {
    public class Command : IRequest<AppUserDto>
    {
      public string UserName { get; set; }
      public string DisplayName { get; set; }
      public int KdTahap { get; set; }
      public string UnitKey { get; set; }
      public string NIP { get; set; }
      public string Password { get; set; }
      public string Description { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(20);
        RuleFor(x => x.DisplayName).MaximumLength(20);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
      }
    }

    public class Handler : IRequestHandler<Command, AppUserDto>
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

      public async Task<AppUserDto> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var existingUser =
          await _context.AppUser.FindAsync(x => x.UserName == request.UserName);

        if (existingUser != null) throw new ApiException("User sudah terpakai");

        var model = _mapper.Map<AppUser>(request);

        model.Password = _passwordHasher.Create(request.Password);

        if (!await _context.AppUser.InsertAsync(model))
          throw new ApiException("Tambah data gagal");

        return _mapper.Map<AppUserDto>(model);
      }
    }
  }
}