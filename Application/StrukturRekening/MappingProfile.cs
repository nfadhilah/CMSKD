using AutoMapper;
using Domain;

namespace Application.StrukturRekening
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
