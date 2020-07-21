using Application.CommonDTO;
using Application.Interfaces;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BPKCQ
{
  public class GetLastDocNumber
  {
    public class Query : IRequest<DocNumber>
    {
      public long IdUnit { get; set; }
      public long IdBend { get; set; }
      public int KdStatus { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
        RuleFor(x => x.IdUnit).NotEmpty();
        RuleFor(x => x.IdBend).NotEmpty();
        RuleFor(x => x.KdStatus).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Query, DocNumber>
    {
      private readonly IBendDocNumGenerator _bendDocNumGenerator;

      public Handler(IBendDocNumGenerator bendDocNumGenerator)
      {
        _bendDocNumGenerator = bendDocNumGenerator;
      }

      public async Task<DocNumber> Handle(
      Query req, CancellationToken cancellationToken)
      {
        var docNumber = await _bendDocNumGenerator.GetBendDocNumber(req.IdUnit,
          req.IdBend, "frmtbpk", req.KdStatus, "BPK", "NOBPK");

        return new DocNumber
        {
          Number = docNumber,
          Type = nameof(BPK)
        };
      }
    }
  }
}
