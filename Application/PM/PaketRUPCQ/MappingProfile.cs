using AutoMapper;
using Domain.PM;

namespace Application.PM.PaketRUPCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, PaketRUP>();
      CreateMap<Update.Command, PaketRUP>();
      CreateMap<PaketRUP, PaketRUPDTO>()
        .ForMember(d => d.KodeRUP, opt => opt.MapFrom(s => s.KodeRUP.Trim()))
        .ForMember(d => d.Lokasi, opt => opt.MapFrom(s => s.Lokasi.Trim()))
        .ForMember(d => d.NmPaket, opt => opt.MapFrom(s => s.NmPaket.Trim()))
        .ForMember(d => d.Volume, opt => opt.MapFrom(s => s.Volume.Trim()))
        .ForMember(d => d.UraiPaket,
          opt => opt.MapFrom(s => s.UraiPaket.Trim()))
        .ForMember(d => d.KdUnit, opt => opt.MapFrom(s => s.Unit.KdUnit.Trim()))
        .ForMember(d => d.NmUnit, opt => opt.MapFrom(s => s.Unit.NmUnit.Trim()))
        .ForMember(d => d.LblStatus, opt => opt.MapFrom(s => s.Status))
        .ForMember(d => d.NuSubKeg,
          opt => opt.MapFrom(s => s.Keg.NuKeg.Trim()))
        .ForMember(d => d.NmSubKeg,
          opt => opt.MapFrom(s => s.Keg.NmKegUnit.Trim()))
        .ForMember(d => d.UraianJnsPekerjaan,
          opt => opt.MapFrom(s => s.JnsPekerjaan.Uraian.Trim()))
        .ForMember(d => d.UraianMetodePengadaan,
          opt => opt.MapFrom(s => s.MetodePengadaan.Uraian.Trim()))
        .ForMember(d => d.KdDana,
          opt => opt.MapFrom(s => s.JDana.KdDana.Trim()))
        .ForMember(d => d.NmDana,
          opt => opt.MapFrom(s => s.JDana.NmDana.Trim()))
        .ForMember(d => d.NmPhk3,
          opt => opt.MapFrom(s => s.Phk3.NmPhk3.Trim()))
        .ForMember(d => d.NmInstPhk3,
          opt => opt.MapFrom(s => s.Phk3.NmInst.Trim()))
        .ForMember(d => d.NPWPPhk3,
          opt => opt.MapFrom(s => s.Phk3.NPWP.Trim()));
    }
  }
}