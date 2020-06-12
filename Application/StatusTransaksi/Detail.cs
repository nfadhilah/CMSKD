using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.StatusTransaksi
{
    public class Detail
    {
        public class Query : IRequest<StatTrs>
        {
            public long IdStatTrs { get; set; }
        }

        public class Handler : IRequestHandler<Query, StatTrs>
        {
            private readonly IDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<StatTrs> Handle(
            Query request, CancellationToken cancellationToken)
            {
                var result =
                 /*await _context.StatTrs.FindByIdAsync(request.IdStatTrs);*/
                 await _context.StatTrs.FindAsync(x => x.IdStatTrs == request.IdStatTrs);

                if (result == null)
                    throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

                return result;
            }
        }
    }
}
