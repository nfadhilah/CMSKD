using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.SPPBPKCQ
{
  public class BulkDelete
  {
    public class Command : IRequest<IEnumerable<SPPBPKDTO>>
    {
      public long IdSPP { get; set; }
      public List<long> IdBPKList { get; set; }
    }

    public class Validation : AbstractValidator<Command>
    {
      public Validation()
      {
        RuleFor(x => x.IdSPP).NotEmpty();
        RuleFor(x => x.IdBPKList).NotEmpty();
      }
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
          await _context.SPPBPK.BulkDeleteAsync("IDBPK", request.IdBPKList,
            transaction);

          await _context.SPPDetR.DeleteAsync(x => x.IdSPP == request.IdSPP,
            transaction);

          var bpkSPPDet =
            await _context.SPPBPK.PopulateSPPDetR(request.IdSPP, transaction);

          var sppDetRList = bpkSPPDet.Select(bpkDet => new SPPDetR
          {
            IdRek = bpkDet.IdRek,
            IdKeg = bpkDet.IdKeg,
            IdNoJeTra = bpkDet.IdNoJeTra,
            IdSPP = request.IdSPP,
            Nilai = bpkDet.Nilai,
            DateCreate = DateTime.Now
          }).ToList();

          if (sppDetRList.Any())
            await _context.SPPDetR.BulkInsertAsync(sppDetRList, transaction);

          transaction.Commit();

          var result = await _context.SPPBPK.FindAllAsync<SPP, BPK>(x =>
            x.IdSPP == request.IdSPP, x => x.SPP, x => x.BPK);

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