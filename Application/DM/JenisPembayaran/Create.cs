using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.DM.JenisPembayaran
{
  public class Create
  {
    public class Command : IRequest<JBayar>
    {
      // public long IdJBayar { get; set; }
      public int KdBayar { get; set; }
      public string UraianBayar { get; set; }
      public DateTime? DateCreate { get; set; }
      public DateTime? DateUpdate { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        // RuleFor(d => d.IdJBayar).NotEmpty();
        RuleFor(d => d.KdBayar).NotEmpty();
        RuleFor(d => d.UraianBayar).NotEmpty();
        RuleFor(d => d.DateCreate).NotEmpty();
        RuleFor(d => d.DateUpdate).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, JBayar>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<JBayar> Handle(
        Command request, CancellationToken cancellationToken)
      {
        var added = _mapper.Map<JBayar>(request);

        if (!await _context.JBayar.InsertAsync(added))
          throw new ApiException("Problem saving changes");

        return added;
      }
    }
  }
}