using Application.Interfaces;
using AutoMapper;

namespace Application.Common.DTOS
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