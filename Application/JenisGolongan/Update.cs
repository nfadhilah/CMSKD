using AutoMapper;
using AutoWrapper.Wrappers;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JenisGolongan
{
    public class Update
    {
        public class Command : IRequest
        {
            public string KdGol { get; set; }
            public long IdGol { get; set; }
            public string NmGol { get; set; }
            public string Pangkat { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(d => d.KdGol).NotEmpty();
                /*RuleFor(d => d.IdGol).NotEmpty();*/
                RuleFor(d => d.NmGol).NotEmpty();
                RuleFor(d => d.Pangkat).NotEmpty();
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
                  /*await _context.Golongan.FindByIdAsync(request.IdGol);*/
                    await _context.Golongan.FindAsync(x => x.IdGol == request.IdGol);

                if (updated == null)
                    throw new ApiException("Not found", (int)HttpStatusCode.NotFound);

                _mapper.Map(request, updated);

                if (!_context.Golongan.Update(updated))
                    throw new ApiException("Problem saving changes");

                return Unit.Value;
            }
        }
    }
}
