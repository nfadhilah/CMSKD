using AutoMapper;
using AutoWrapper.Wrappers;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.StatusTransaksi
{
    public class Create
    {
        public class Command : IRequest<StatTrs>
        {
            public string KdStatus { get; set; }
            //public long IdStatTrs { get; set; }
            public string LblStatus { get; set; }
            public string Uraian { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(d => d.KdStatus).NotEmpty();
                //RuleFor(d => d.IdStatTrs).NotEmpty();
                RuleFor(d => d.LblStatus).NotEmpty();
                RuleFor(d => d.Uraian).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, StatTrs>
        {
            private readonly IDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<StatTrs> Handle(
              Command request, CancellationToken cancellationToken)
            {
                var added = _mapper.Map<StatTrs>(request);

                if (!await _context.StatTrs.InsertAsync(added))
                    throw new ApiException("Problem saving changes");

                return added;
            }
        }
    }
}
