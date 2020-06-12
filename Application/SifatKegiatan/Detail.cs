using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SifatKegiatan
{
	public class Detail
	{
		public class Query : IRequest<SifatKeg>
		{
			public long IdSifatKeg { get; set; }
		}

		public class Handler : IRequestHandler<Query, SifatKeg>
		{
			private readonly IDbContext _context;
			private readonly IMapper _mapper;

			public Handler(IDbContext context, IMapper mapper)
			{
				_context = context;
				_mapper = mapper;
			}

			public async Task<SifatKeg> Handle(
			Query request, CancellationToken cancellationToken)
			{
				var result =
				  await _context.SifatKeg.FindByIdAsync(request.IdSifatKeg);

				if (result == null)
					throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

				return result;
			}
		}
	}
}
