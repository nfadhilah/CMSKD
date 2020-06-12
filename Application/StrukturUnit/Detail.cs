using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.StrukturUnit
{
    public class Detail
    {
        public class Query : IRequest<StruUnit>
        {
            public long IdStruUnit { get; set; }
        }

        public class Handler : IRequestHandler<Query, StruUnit>
        {
            private readonly IDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<StruUnit> Handle(
            Query request, CancellationToken cancellationToken)
            {
                var result =
                 /*await _context.StruUnit.FindByIdAsync(request.IdStruUnit);*/
                 await _context.StruUnit.FindAsync(x => x.IdStruUnit == request.IdStruUnit);

                if (result == null)
                    throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

                return result;
            }
        }
    }
}
