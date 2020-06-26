using System;

namespace Application.CommonDTO
{
  public class Pagination
  {
    public int TotalItemsCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }

    public int TotalPages
    {
      get
      {
        if (PageSize == 0) return 0;

        return (int)Math.Ceiling((double)TotalItemsCount / PageSize);
      }
    }
  }
}