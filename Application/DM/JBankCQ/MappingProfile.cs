using AutoMapper;
using Domain.DM;

namespace Application.DM.JBankCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, JBank>();
      CreateMap<Update.Command, JBank>();
    }
  }
}
