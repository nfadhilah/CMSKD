using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.StruRekCQ
{
    public class Update
    {
        public class Command : IRequest
        {
            public int? MtgLevel { get; set; }
            public long IdStruRek { get; set; }
            public string NmLevel { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(d => d.MtgLevel).NotEmpty();
                /*RuleFor(d => d.IdStruRek).NotEmpty();*/
                RuleFor(d => d.NmLevel).NotEmpty();
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
                    /*await _context.StruRek.FindByIdAsync(request.IdStruRek);*/
                    await _context.StruRek.FindAsync(x => x.IdStruRek == request.IdStruRek);

                if (updated == null)
                    throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

                _mapper.Map(request, updated);

                if (!_context.StruRek.Update(updated))
                    throw new ApiException("Problem saving changes");

                return Unit.Value;
            }
        }
    }
}
