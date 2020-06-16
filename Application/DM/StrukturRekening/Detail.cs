using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using MediatR;
using Persistence;

namespace Application.DM.StrukturRekening
{
    public class Detail
    {
        public class Query : IRequest<StruRek>
        {
            public long IdStruRek { get; set; }
        }

        public class Handler : IRequestHandler<Query, StruRek>
        {
            private readonly IDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<StruRek> Handle(
            Query request, CancellationToken cancellationToken)
            {
                var result =
                 /*await _context.StruRek.FindByIdAsync(request.IdStruRek);*/
                 await _context.StruRek.FindAsync(x => x.IdStruRek == request.IdStruRek);

                if (result == null)
                    throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

                return result;
            }
        }
    }
}
