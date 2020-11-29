using Domain.Auth;

namespace Application.Auth.WebUserCQ
{
  public class MappingProfile : AutoMapper.Profile
  {
    public MappingProfile()
    {
      CreateMap<WebUser, WebUserDTO>()
        .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId.Trim()))
        .ForMember(d => d.BlokId, opt => opt.MapFrom(s => string.IsNullOrWhiteSpace(s.BlokId) ? "0" : s.BlokId))
        .ForMember(d => d.NmGroup, opt => opt.MapFrom(s => s.WebGroup.NmGroup));
      CreateMap<Create.Command, WebUser>();
      CreateMap<Update.Command, WebUser>();
    }
  }
}