using AutoMapper;
using Dapper;
using FluentValidation;
using MediatR;
using Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPCQ
{
  public class ListGroupBySubKeg
  {
    public class DTO
    {
      public long IdSubKegUnit { get; set; }
      public string Kode { get; set; }
      public long IdUnit { get; set; }
      public string NmKegUnit { get; set; }
      public string NmSubKegUnit { get; set; }
      public decimal Pagu { get; set; }
      public decimal Realisasi { get; set; }
    }

    public class Query : IRequest<IEnumerable<DTO>>
    {
      public long IdUnit { get; set; }
      public int KdTahap { get; set; }
    }

    public class Validator : AbstractValidator<Query>
    {
      public Validator()
      {
        RuleFor(x => x.IdUnit).NotEmpty();
        RuleFor(x => x.KdTahap).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<DTO>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<DTO>> Handle(
        Query request, CancellationToken cancellationToken)
      {
        var result = await _context.Connection.QueryAsync<DTO>(
          @"SELECT k.IDKEGUNIT as IdSubKegUnit, 
       RTRIM(d.KDURUS) + RTRIM(m2.NUPRGRM) + RTRIM(m3.NUKEG) + RTRIM(m.NUKEG) AS Kode,
       k.IDUNIT as IdUnit,
       m3.NMKEGUNIT as NmKegUnit,
       m.NMKEGUNIT as NmSubKegUnit,
       SUM(k.PAGU) AS Pagu,
       SUM(ISNULL(s.NSPP, 0)) AS Realisasi
FROM dbo.KEGUNIT k
    INNER JOIN dbo.MKEGIATAN m
        ON m.IDKEG = k.IDKEG
    INNER JOIN dbo.MKEGIATAN m3
        ON m3.IDKEG = m.IDKEGINDUK
    INNER JOIN dbo.MPGRM m2
        ON m2.IDPRGRM = m.IDPRGRM
    LEFT JOIN dbo.DAFTURUS d
        ON d.IDURUS = m2.IDURUS
    LEFT JOIN
    (
        SELECT s.IDKEG,
               SUM(s.NILAI) AS NSPP
        FROM dbo.SPPDETR s
            INNER JOIN dbo.SPP s2
                ON s2.IDSPP = s.IDSPP
        WHERE s2.KDSTATUS = 24
              AND s2.IDUNIT = @IdUnit
        GROUP BY s.IDKEG
    ) AS s
        ON k.IDKEG = s.IDKEG
WHERE k.IDUNIT = @IdUnit
      AND k.KDTAHAP = @KdTahap
GROUP BY k.IDKEGUNIT, k.IDUNIT,
         m3.NMKEGUNIT,
         m.NMKEGUNIT,
         RTRIM(d.KDURUS) + RTRIM(m2.NUPRGRM) + RTRIM(m3.NUKEG) + RTRIM(m.NUKEG);",
          new { request.IdUnit, request.KdTahap });

        return result;
      }
    }
  }
}