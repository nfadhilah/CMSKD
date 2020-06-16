using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain.DM
{
  [Table("JBAYAR")]
  public class JBayar
  {
    [Key, Identity]
    public long IdJBayar { get; set; }
    [Key]
    public int KdBayar { get; set; }
    public string UraianBayar { get; set; }
    public DateTime? DateCreate { get; set; }
    public DateTime? DateUpdate { get; set; }
  }
}
