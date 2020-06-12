using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SifatKegiatan
{
    public class Create
    {
        public class Command : IRequest<SifatKeg>
        {
            //public long IdSifatKeg { get; set; }
            public string KdSifat { get; set; }
            public string NmSifat { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                // RuleFor(d => d.IdSifatKeg).NotEmpty();
                RuleFor(d => d.KdSifat).NotEmpty();
                RuleFor(d => d.NmSifat).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, SifatKeg>
        {
            private readonly IDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<SifatKeg> Handle(
              Command request, CancellationToken cancellationToken)
            {
                var added = _mapper.Map<SifatKeg>(request);

                if (!await _context.SifatKeg.InsertAsync(added))
                    throw new ApiException("Problem saving changes");

                return added;
            }
        }
    }
}
