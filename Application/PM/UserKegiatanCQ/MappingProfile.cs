using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.PM;

namespace Application.PM.UserKegiatanCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<UserKegiatan, UserKegiatanDTO>()
        .ForMember(d => d.Nama,
          opt => opt.MapFrom(s => s.WebUser.Nama.Trim()))
        .ForMember(d => d.NuKeg,
          opt => opt.MapFrom(s => s.MKegiatan.NuKeg.Trim()))
        .ForMember(d => d.NmKegUnit,
          opt => opt.MapFrom(s => s.MKegiatan.NmKegUnit.Trim()));
    }
  }
}