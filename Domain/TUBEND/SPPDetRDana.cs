using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.TUBEND
{
  [Table("SPPDETRDANA")]
  public class SPPDetRDana
  {
    [Key, Identity]
    public long IdSPPDetRDana { get; set; }
    public long IdSPPDetR { get; set; }
    [InnerJoin("SPPDETR", "IDSPPDETR", "IDSPPDETR")]
    public SPPDetR SPPDetR { get; set; }
    public string KdDana { get; set; }
    [LeftJoin("JDANA", "KDDANA", "KDDANA")]
    public JDana JDana { get; set; }
    public decimal? Nilai { get; set; }
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}