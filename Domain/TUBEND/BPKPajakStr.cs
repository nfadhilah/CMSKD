using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("BPKPAJAKSTR")]
  public class BPKPajakStr
  {
    [Key, Identity]
    public long IdBkPajakStr { get; set; }
    public long? IdBPKDetRP { get; set; }
    [LeftJoin("BPKDETRP", "IDBPKDETRP", "IDBPKDETRP")]
    public BPKDetRP BPKDetRp { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}