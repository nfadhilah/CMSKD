using AutoMapper;
using Domain;

namespace Application.StatusTransaksi
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Create.Command, StatTrs>();
			CreateMap<Update.Command, StatTrs>();
		}
	}
}
