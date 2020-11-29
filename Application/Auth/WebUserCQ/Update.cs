using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.Auth;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Auth.WebUserCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;
      public int KdTahap { get; set; }
      public string UnitKey { get; set; }
      public string NIP { get; set; }
      public string GroupId { get; set; }
      public string Pwd { get; set; }
      public string DigitalIdFile { get; set; }
      public string DigitalIdPwd { get; set; }
      public string Nama { get; set; }
      public int? BlokId { get; set; }
      public int? TranSecure { get; set; }
      public int? StInsert { get; set; }
      public int? StUpdate { get; set; }
      public int? StDelete { get; set; }
      public string Ket { get; set; }
      public string SignImg { get; set; }
      public string Photo { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt => { opt.CreateMap<DTO, Command>(); });

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
        RuleFor(x => x.KdTahap).NotEmpty();
        RuleFor(x => x.UnitKey).NotEmpty();
        RuleFor(x => x.NIP).NotEmpty();
        RuleFor(x => x.GroupId).NotEmpty();
        RuleFor(x => x.Pwd).NotEmpty().MinimumLength(6);
        RuleFor(x => x.DigitalIdPwd).NotEmpty().MinimumLength(6);
        RuleFor(x => x.Nama).NotEmpty();
      }
    }

    public class Command : WebUser, IRequest<WebUserDTO>
    {
    }

    public class Handler : IRequestHandler<Command, WebUserDTO>
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

      public async Task<WebUserDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.WebUser.FindByIdAsync(request.UserId);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!string.IsNullOrWhiteSpace(request.Pwd))
        {
          updated.Pwd = _passwordHasher.Create(request.Pwd);
          updated.DigitalIdPwd = _passwordHasher.Create(request.DigitalIdPwd);
        }

        if (!await _context.WebUser.UpdateAsync(updated))
          throw new ApiException("Tambah data gagal");

        var result =
          await _context.WebUser.FindAsync<WebGroup>(x => x.UserId == request.UserId, x => x.WebGroup);

        return _mapper.Map<WebUserDTO>(result);
      }
    }
  }
}