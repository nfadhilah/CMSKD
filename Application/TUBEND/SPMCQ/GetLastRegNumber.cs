using Application.CommonDTO;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPMCQ
{
  public class GetLastRegNumber
  {
    public class Query : IRequest<RegNumber>
    {
      public long IdUnit { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
        RuleFor(x => x.IdUnit).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Query, RegNumber>
    {
      private readonly IBendDocNumGenerator _bendDocNumGenerator;

      public Handler(IBendDocNumGenerator bendDocNumGenerator)
      {
        _bendDocNumGenerator = bendDocNumGenerator;
      }

      public async Task<RegNumber> Handle(
      Query req, CancellationToken cancellationToken)
      {
        var regNumber = await _bendDocNumGenerator.GetRegNumber(req.IdUnit, "SPM");

        return new RegNumber
        {
          NoReg = regNumber
        };
      }
    }
  }
}
