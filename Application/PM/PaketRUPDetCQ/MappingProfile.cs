using AutoMapper;
using Domain.PM;

namespace Application.PM.PaketRUPDetCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, PaketRUPDet>();
      CreateMap<Update.Command, PaketRUPDet>();
      CreateMap<PaketRUPDet, PaketRUPDetDTO>()
        .ForMember(d => d.KodeRUP, opt => opt.MapFrom(s => s.KodeRUP.Trim()))
        .ForMember(d => d.Lokasi, opt => opt.MapFrom(s => s.Lokasi.Trim()))
        .ForMember(d => d.NmPaket, opt => opt.MapFrom(s => s.NmPaket.Trim()))
        .ForMember(d => d.Volume, opt => opt.MapFrom(s => s.Volume.Trim()))
        .ForMember(d => d.UraiPaket,
          opt => opt.MapFrom(s => s.UraiPaket.Trim()))
        .ForMember(d => d.JnsPekerjaan,
          opt => opt.MapFrom(s => s.JnsPekerjaan.Uraian.Trim()))
        .ForMember(d => d.KdDana,
          opt => opt.MapFrom(s => s.JDana.KdDana.Trim()))
        .ForMember(d => d.NmDana,
          opt => opt.MapFrom(s => s.JDana.NmDana.Trim()))
        .ForMember(d => d.NmPhk3,
          opt => opt.MapFrom(s => s.Phk3.NmPhk3.Trim()))
        .ForMember(d => d.NmInst,
          opt => opt.MapFrom(s => s.Phk3.NmInst.Trim()));
    }
  }
}