using Domain.Auth;

namespace Application.Auth.User
{
  public class MappingProfile : AutoMapper.Profile
  {
    public MappingProfile()
    {
      CreateMap<WebUser, WebUserDto>()
        .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId.Trim()))
        .ForMember(d => d.NmUnit,
          opt => opt.MapFrom(s => s.DaftUnit.NmUnit.Trim()))
        .ForMember(d => d.NmGroup,
          opt => opt.MapFrom(s => s.WebGroup.NmGroup.Trim()))
        .ForMember(d => d.NmPegawai, opt => opt.MapFrom(s => s.Pegawai.Nama))
        .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email.Trim()))
        .ForMember(d => d.NIPPegawai,
          opt => opt.MapFrom(s => s.Pegawai.NIP.Trim()));
      CreateMap<Register.Command, WebUser>()
        .ForMember(d => d.Pwd, opt => opt.Ignore());
      CreateMap<Update.Command, WebUser>()
        .ForMember(d => d.Pwd, opt => opt.Ignore());
    }
  }
}