namespace Application.Interfaces
{
  public interface IMapDTO<in T, TOut>
  {
    TOut MapDTO(T dto, TOut destination);
  }
}
