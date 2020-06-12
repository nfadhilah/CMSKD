using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.StrukturRekening
{
    public class Delete
    {
        public class Command : IRequest
        {
            public long IdStruRek { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
            }
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
                var deleted =
                  /*await _context.StruRek.FindByIdAsync(request.IdStruRek);*/
                  await _context.StruRek.FindAsync(x => x.IdStruRek == request.IdStruRek);

                if (deleted == null)
                    throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

                if (!_context.StruRek.Delete(deleted))
                    throw new ApiException("Problem saving changes");

                return Unit.Value;
            }
        }
    }
}
