using AutoMapper;
using Domain.DM;

namespace Application.DM.DocMetaCQ
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Create.Command, DocMeta>();
    }
  }
}
