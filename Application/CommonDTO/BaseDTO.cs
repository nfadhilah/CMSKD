using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CommonDTO
{
  public class BaseDTO<T> : IMapDTO<T>
  {
    private readonly IMapper _mapper;

    public BaseDTO()
    {
      var config = new MapperConfiguration(opt =>
      {
        opt.CreateMap(typeof(BaseDTO<T>), typeof(T));
      });

      _mapper = config.CreateMapper();
    }

    public T MapDTO(T destination)
    {
      return _mapper.Map(this, destination);
    }
  }
}