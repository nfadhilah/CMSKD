using AutoMapper;
using Domain.DM;

namespace Application.DM.StrukturRekening
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
