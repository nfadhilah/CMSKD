using Application.TUBEND.BkBankDetCQ;
using AutoMapper;
using AutoWrapper.Wrappers;
using Domain.DM;
using Domain.TUBEND;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TUBEND.BkBankCQ
{
  public class CreateHeaderDetail
  {
    public class Command : IRequest<BKBankHeaderDetailDTO>
    {
      public long IdUnit { get; set; }
      public long IdBend { get; set; }
      public string NoBuku { get; set; }
      public string KdStatus { get; set; }
      public DateTime? TglBuku { get; set; }
      public string Uraian { get; set; }
      public DateTime? TglValid { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
      public Validator()
      {
        RuleFor(d => d.IdUnit).NotEmpty();
        RuleFor(d => d.IdBend).NotEmpty();
        RuleFor(d => d.NoBuku).NotEmpty();
        RuleFor(d => d.KdStatus).NotEmpty();
      }
    }

    public class Handler : IRequestHandler<Command, BKBankHeaderDetailDTO>
    {
      private readonly IDbContext _context;
      private readonly IMapper _mapper;

      public Handler(IDbContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<BKBankHeaderDetailDTO> Handle(
        Command request, CancellationToken cancellationToken)
      {
        using var transaction = _context.Connection.BeginTransaction();

        try
        {
          var header = _mapper.Map<BkBank>(request);

          var mapNoJeTra = new Dictionary<string, int>
          { ["33"] = 31, ["34"] = 32 };

          await _context.BkBank.InsertAsync(header, transaction);

          var detail = new BkBankDet
          {
            IdBkBank = header.IdBkBank,
            IdNoJeTra = mapNoJeTra[header.KdStatus],
            Nilai = 0
          };

          await _context.BkBankDet.InsertAsync(detail, transaction);

          var headerResult = await _context.BkBank
            .FindAllAsync<DaftUnit, Bend, StatTrs>(
              x => x.IdBkBank == header.IdBkBank, x => x.Unit,
              x => x.Bend, x => x.StatTrs, transaction);

          transaction.Commit();

          var resultDTO = new BKBankHeaderDetailDTO
          {
            Header = _mapper.Map<BKBankDTO>(headerResult.Single()),
            Detail = new BKBankDetDTO
            {
              IdBankDet = detail.IdBankDet,
              NoBuku = header.NoBuku,
              IdBkBank = detail.IdBkBank,
              IdNoJeTra = detail.IdNoJeTra,
              Nilai = 0
            }
          };

          return resultDTO;
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