using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPBPKCQ
{
  public class BulkInsert
  {
    public class Command : IRequest<IEnumerable<SPPBPKDTO>>
    {
      public IEnumerable<Create.Command> Data { get; set; }
    }

    public class Handler : IRequestHandler<Command, IEnumerable<SPPBPKDTO>>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<IEnumerable<SPPBPKDTO>> Handle(
        Command request, CancellationToken cancellationToken)
      {
        using var transaction = _context.Connection.BeginTransaction();

        try
        {
          var sppDetRList = new List<SPPDetR>();

          var sppBPKList = _mapper.Map<List<SPPBPK>>(request.Data);

          await _context.SPPBPK.BulkInsertAsync(sppBPKList, transaction);

          foreach (var sppbpk in sppBPKList)
          {
            var bpkDetList =
              await _context.BPKDetR.FindAllAsync(
                x => x.IdBPK == sppbpk.IdBPK, transaction);

            sppDetRList.AddRange(bpkDetList.Select(bpkDet => new SPPDetR
            {
              IdKeg = bpkDet.IdKeg,
              IdNoJeTra = bpkDet.IdNoJeTra,
              IdRek = bpkDet.IdRek,
              IdSPP = sppbpk.IdSPP,
              Nilai = bpkDet.Nilai,
              DateCreate = DateTime.Now
            }));

            await _context.SPPDetR.BulkInsertAsync(sppDetRList, transaction);
          }

          transaction.Commit();

          var result = await _context.SPPBPK.FindAllAsync<SPP, BPK>(x =>
            x.IdSPP == request.Data.Single().IdSPP, x => x.SPP, x => x.BPK);

          return _mapper.Map<IEnumerable<SPPBPKDTO>>(result);
        }
        catch (Exception e)
        {
          transaction.Rollback();

          throw new ApiException(e.Message);
        }
      }
    }
  }
}