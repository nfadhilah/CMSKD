using AutoMapper;
using Domain.DM;

namespace Application.DM.DaftPhk3CQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DaftPhk3>();
      CreateMap<Update.Command, DaftPhk3>();
      CreateMap<DaftPhk3, DaftPhk3DTO>()
        .ForMember(d => d.NoRekBank, opt => opt.MapFrom(s => s.NoRekBank.Trim()))
        .ForMember(d => d.NPWP, opt => opt.MapFrom(s => s.NPWP.Trim()))
        .ForMember(d => d.KdBank, opt => opt.MapFrom(s => s.Bank.KdBank.Trim()))
        .ForMember(d => d.NmBank, opt => opt.MapFrom(s => s.Bank.NmBank.Trim()))
        .ForMember(d => d.UraianBank,
          opt => opt.MapFrom(s => s.Bank.Uraian.Trim()))
        .ForMember(d => d.AkronimBank,
          opt => opt.MapFrom(s => s.Bank.Akronim.Trim()))
        .ForMember(d => d.BadanUsaha,
          opt => opt.MapFrom(s => s.JUsaha.BadanUsaha.Trim()))
        .ForMember(d => d.KeteranganUsaha,
          opt => opt.MapFrom(s => s.JUsaha.Keterangan.Trim()))
        .ForMember(d => d.AkronimUsaha,
          opt => opt.MapFrom(s => s.JUsaha.Akronim.Trim()));
    }
  }
}