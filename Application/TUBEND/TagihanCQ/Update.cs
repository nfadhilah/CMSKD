using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.TagihanCQ
{
  public class Update
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdUnit { get; set; }
      public long IdKeg { get; set; }
      public string NoTagihan { get; set; }
      public DateTime TglTagihan { get; set; }
      public long IdKontrak { get; set; }
      public string UraianTagihan { get; set; }
      public DateTime? TglValid { get; set; }
      public string KdStatus { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }

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
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdKeg).NotEmpty();
        RuleFor(d => d.NoTagihan).NotEmpty();
        RuleFor(d => d.TglTagihan).NotEmpty();
        RuleFor(d => d.IdKontrak).NotEmpty();
      }
    }

    public class Command : Tagihan, IRequest<TagihanDTO>
    {
    }

    public class Handler : IRequestHandler<Command, TagihanDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<TagihanDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var updated =
          await _context.Tagihan.FindByIdAsync(request.IdTagihan);

        if (updated == null)
          throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

        _mapper.Map(request, updated);

        if (!_context.Tagihan.Update(updated))
          throw new ApiException("Problem saving changes");

        var result = await _context.Tagihan
          .FindAllAsync<Kontrak>(
            x => x.IdTagihan == updated.IdTagihan,
            x => x.Kontrak);

        return _mapper.Map<TagihanDTO>(result.FirstOrDefault());
      }
    }
  }
}