using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.BUD.BKUDCQ;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Auth;
using Domain.BUD;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Auth.User
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

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
      public bool IsAuthorized { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt =>
        {
          opt.CreateMap<DTO, Command>();
        });

        _mapper = config.CreateMapper();
      }

      public Command MapDTO(Command destination)
      {
        return _mapper.Map(this, destination);
      }
    }

    public class Validator : AbstractValidator<DTO>
    {
      public Validator()
      {
        RuleFor(x => x.Nama).NotEmpty();
        // RuleFor(x => x.Pwd).NotEmpty().MinimumLength(6);
      }
    }

    public class Command : WebUser, IRequest<WebUserDto> { }

    public class Handler : IRequestHandler<Command, WebUserDto>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;
      private readonly IPasswordHasher _passwordHasher;
      private readonly IUserAccessor _userAccessor;

      public Handler(
        IDbContext context, IMapper mapper, IPasswordHasher passwordHasher,
        IUserAccessor userAccessor)
      {
        _context = context;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _userAccessor = userAccessor;
      }

      public async Task<WebUserDto> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.WebUser.FindByIdAsync(request.UserId);

        if (updated == null)
          throw new ApiException("Not found", (int) HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        updated.Pwd = _passwordHasher.Create(request.Pwd);

        if (updated.IsAuthorized.HasValue && updated.IsAuthorized.Value)
        {
          updated.AuthorizedBy = _userAccessor.GetCurrentUsername();
          updated.AuthorizedDate = DateTime.Now;
        }

        if (!await _context.WebUser.InsertAsync(updated))
          throw new ApiException("Tambah data gagal");

        var result =
          await _context.WebUser.GetUserWithRelation(updated.UserId);

        return _mapper.Map<WebUserDto>(result);
      }
    }
  }
}