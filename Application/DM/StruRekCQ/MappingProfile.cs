using AutoMapper;
using Domain.DM;

namespace Application.DM.StruRekCQ
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Create.Command, StruRek>();
			CreateMap<Update.Command, StruRek>();
		}
	}
}
