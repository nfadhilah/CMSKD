using AutoMapper;
using Domain;

namespace Application.StrukturUnit
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Create.Command, StruUnit>();
			CreateMap<Update.Command, StruUnit>();
		}
	}
}
