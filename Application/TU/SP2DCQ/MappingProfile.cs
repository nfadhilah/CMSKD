using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.TU;

namespace Application.TU.SP2DCQ
{
    public class MappingProfile : Profile
    {
      public MappingProfile()
      {
        CreateMap<SP2D, SP2DDTO>();
      }
    }
}
