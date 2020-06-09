using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
  [Table("JNSAKUN")]
  public class JnsAkun
  {
    [Key, Identity]
    public long IdJnsAkun { get; set; }
    public string UraiAkun { get; set; }
    public string KdPers { get; set; }
  }
}
