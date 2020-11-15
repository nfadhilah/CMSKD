using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.TU;

namespace Application.TU.SP2DCQ
{
    public class MappingProfile : Profile
    {
      public MappingProfile()
      {
        CreateMap<SP2D, SP2DDTO>()
          .ForMember(d => d.LblStatus, opt => opt.MapFrom(s => s.StatTrs.LblStatus))
          .ForMember(d => d.UnitKey, opt => opt.MapFrom(s => s.UnitKey.Trim()))
          .ForMember(d => d.NoSP2D, opt => opt.MapFrom(s => s.NoSP2D.Trim()))
          .ForMember(d => d.NoSPM, opt => opt.MapFrom(s => s.NoSPM.Trim()))
          .ForMember(d => d.KeyBend, opt => opt.MapFrom(s => s.KeyBend.Trim()))
          .ForMember(d => d.IdxSKO, opt => opt.MapFrom(s => s.IdxSKO.Trim()))
          .ForMember(d => d.IdxTTD, opt => opt.MapFrom(s => s.IdxTTD.Trim()))
          .ForMember(d => d.KdP3, opt => opt.MapFrom(s => s.KdP3.Trim()))
          .ForMember(d => d.NoKontrak, opt => opt.MapFrom(s => s.NoKontrak.Trim()))
          .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.DaftUnit.KdUnit.Trim()))
          .ForMember(d => d.NmUnit, opt => opt.MapFrom(s => s.DaftUnit.NmUnit.Trim()));
      }
    }
}
