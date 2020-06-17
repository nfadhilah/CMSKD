using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.SifatKegCQ
{
	public class Update
	{
		public class DTO : IMapDTO<Command>
		{
			private readonly IMapper _mapper;

			public string KdSifat { get; set; }
			public string NmSifat { get; set; }

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
				RuleFor(d => d.KdSifat).NotEmpty();
				RuleFor(d => d.NmSifat).NotEmpty();
			}
		}

		public class Command : IRequest
		{
			public long IdSifatKeg { get; set; }
			public string KdSifat { get; set; }
			public string NmSifat { get; set; }
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
				var updated =
				  await _context.SifatKeg.FindByIdAsync(request.IdSifatKeg);

				if (updated == null)
					throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

				_mapper.Map(request, updated);

				if (!_context.SifatKeg.Update(updated))
					throw new ApiException("Problem saving changes");

				return Unit.Value;
			}
		}
	}
}
