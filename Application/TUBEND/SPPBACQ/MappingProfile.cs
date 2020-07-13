using AutoMapper;
using Domain.TUBEND;

namespace Application.TUBEND.SPPBACQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, Berita>();
      CreateMap<Update.Command, Berita>();
      CreateMap<SPPBA, SPPBADTO>()
        .ForMember(s => s.NoSPP, opt => opt.MapFrom(s => s.SPP.NoSPP.Trim()))
        .ForMember(s => s.NoBerita,
          opt => opt.MapFrom(s => s.Berita.NoBerita.Trim()));
    }
  }
}
