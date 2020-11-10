using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Auth;

namespace Application.Auth.WebOtorCQ
{
    public class MappingProfile : Profile
    {
      public MappingProfile()
      {
        CreateMap<WebOtor, WebOtorDTO>()
          .ForMember(d => d.NmGroup, opt => opt.MapFrom(s => s.WebGroup.NmGroup.Trim()))
          .ForMember(d => d.KetGroup, opt => opt.MapFrom(s => s.WebGroup.Ket.Trim()))
          .ForMember(d => d.NmRole, opt => opt.MapFrom(s => s.WebRole.Role.Trim()))
          .ForMember(d => d.Type, opt => opt.MapFrom(s => s.WebRole.Type.Trim()))
          .ForMember(d => d.MenuId, opt => opt.MapFrom(s => s.WebRole.MenuId))
          .ForMember(d => d.RouterLink, opt => opt.MapFrom(s => s.WebRole.RouterLink))
          .ForMember(d => d.KdLevel, opt => opt.MapFrom(s => s.WebRole.KdLevel))
          .ForMember(d => d.Icon, opt => opt.MapFrom(s => s.WebRole.Icon))
          .ForMember(d => d.Bantuan, opt => opt.MapFrom(s => s.WebRole.Bantuan));
      }
    }
}
