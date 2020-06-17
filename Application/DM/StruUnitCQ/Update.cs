using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.StruUnitCQ
{
    public class Update
    {
        public class Command : IRequest
        {
            public int? KdLevel { get; set; }
            public long IdStruUnit { get; set; }
            public string NmLevel { get; set; }
            public string NumDigit { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(d => d.KdLevel).NotEmpty();
                /*RuleFor(d => d.IdStruUnit).NotEmpty();*/
                RuleFor(d => d.NmLevel).NotEmpty();
                RuleFor(d => d.NumDigit).NotEmpty();
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
                    /*await _context.StruUnit.FindByIdAsync(request.IdStruUnit);*/
                    await _context.StruUnit.FindAsync(x => x.IdStruUnit == request.IdStruUnit);

                if (updated == null)
                    throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

                _mapper.Map(request, updated);

                if (!_context.StruUnit.Update(updated))
                    throw new ApiException("Problem saving changes");

                return Unit.Value;
            }
        }
    }
}
