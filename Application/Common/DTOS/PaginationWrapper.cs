namespace Application.Common.DTOS
{
  public class PaginationWrapper
  {
    public Pagination Pagination { get; set; }
    public object Data { get; set; }

    public PaginationWrapper(
      object data = null,
      Pagination pagination = null)
    {
      Data = data;
      Pagination = pagination;
    }
  }
}