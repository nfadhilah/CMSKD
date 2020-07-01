using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("BPKDETRDANA")]
  public class BPKDetRDana
  {
    [Key, Identity]
    public long IdBPKDetRDana { get; set; }
    public long IdBPKDetR { get; set; }
    [InnerJoin("BPKDETR", "IDBPKDETR", "IDBPKDETR")]
    public BPKDetR BPKDetR { get; set; }
    public long IdJDana { get; set; }
    [InnerJoin("JDANA", "IDJDANA", "IDJDANA")]
    public JDana JDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}