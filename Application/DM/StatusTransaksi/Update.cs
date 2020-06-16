using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.StatusTransaksi
{
    public class Update
    {
        public class Command : IRequest
        {
            public string? KdStatus { get; set; }
            public long IdStatTrs { get; set; }
            public string LblStatus { get; set; }
            public string Uraian { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(d => d.KdStatus).NotEmpty();
                /*RuleFor(d => d.IdStatTrs).NotEmpty();*/
                RuleFor(d => d.LblStatus).NotEmpty();
                RuleFor(d => d.Uraian).NotEmpty();
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
                var updated =
                    /*await _context.StatTrs.FindByIdAsync(request.IdStatTrs);*/
                    await _context.StatTrs.FindAsync(x => x.IdStatTrs == request.IdStatTrs);

                if (updated == null)
                    throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

                _mapper.Map(request, updated);

                if (!_context.StatTrs.Update(updated))
                    throw new ApiException("Problem saving changes");

                return Unit.Value;
            }
        }
    }
}
