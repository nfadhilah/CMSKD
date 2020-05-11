using System;

namespace Application.Dtos
{
  public class PaginationQuery
  {
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    internal uint Limit => Convert.ToUInt32(PageSize);
    internal uint Offset => ((uint)CurrentPage - 1) * (uint)PageSize;
  }
}
