using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisGolongan
{
    public class Detail
    {
        public class Query : IRequest<Golongan>
        {
            public long IdGol { get; set; }
        }

        public class Handler : IRequestHandler<Query, Golongan>
        {
            private readonly IDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Golongan> Handle(
            Query request, CancellationToken cancellationToken)
            {
                var result =
                 /*await _context.Golongan.FindByIdAsync(request.IdGol);*/
                 await _context.Golongan.FindAsync(x => x.IdGol == request.IdGol);

                if (result == null)
                    throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

                return result;
            }
        }
    }
}
