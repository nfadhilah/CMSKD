﻿using Application.Interfaces;
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
  public class BulkInsert
  {
    public class DTO : IMapDTO<Command>
    {
      private readonly IMapper _mapper;

      public long IdSPP { get; set; }
      public IEnumerable<long> IdBPKList { get; set; }

      public DTO()
      {
        var config = new MapperConfiguration(opt =>
          opt.CreateMap<DTO, Command>().ForMember(x => x.IdBPKList,
            o => o.MapFrom(s => s.IdBPKList)));

        _mapper = config.CreateMapper();
      }

      public Command MapDTO(Command destination)
      {
        return _mapper.Map(this, destination);
      }
    }

    public class Command : IRequest<IEnumerable<SPPBPKDTO>>
    {
      public long IdSPP { get; set; }
      public List<long> IdBPKList { get; set; }
    }

    public class Validator : AbstractValidator<DTO>
    {
      public Validator()
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
          var sppBPKList = request.IdBPKList
            .Select(idBPK => new SPPBPK { IdBPK = idBPK, IdSPP = request.IdSPP });

          await _context.SPPBPK.BulkInsertAsync(sppBPKList, transaction);

          var bpkSPPDet =
            await _context.SPPBPK.PopulateSPPDetRAsync(request.IdSPP, transaction);

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