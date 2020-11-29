using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("DOCMETA")]
  public class DocMeta
  {
    [Key, Identity]
    public int Id { get; set; }
    [LeftJoin("DOCMETA", "KDDOK", "KDDOK")]

    public string KdDok { get; set; }
    public string NoDok { get; set; }
    public string FilePath { get; set; }
    public int? Status { get; set; }
    public DateTime? DateCreated { get; set; }
  }
}