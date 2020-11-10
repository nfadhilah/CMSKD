using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auth;

namespace Application.Auth.WebUserCQ
{
    public class MappingProfile : AutoMapper.Profile
    {
      public MappingProfile()
      {
        CreateMap<WebUser, WebUserDTO>()
          .ForMember(d => d.NmGroup, opt => opt.MapFrom(s => s.WebGroup.NmGroup));
      }
    }
}
