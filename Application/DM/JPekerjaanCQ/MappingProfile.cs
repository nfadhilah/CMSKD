using AutoMapper;
using Domain.DM;

namespace Application.DM.JPekerjaanCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JPekerjaan>();
      CreateMap<Update.Command, JPekerjaan>();
    }
  }
}