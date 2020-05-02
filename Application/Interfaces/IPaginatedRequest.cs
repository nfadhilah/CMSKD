namespace Application.Interfaces
{
  public interface IPaginatedRequest
  {
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public uint GetOffset(int pageSize, int currentPage);
  }
}
