using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.BUD
{
  [Table("DPDET")]
  public class DPDet
  {
    [Key, Identity]
    public long IdDPDet { get; set; }
    public long IdDP { get; set; }
    public long IdSP2D { get; set; }
    [InnerJoin("SP2D", "IDSP2D", "IDSP2D")]
    public SP2D SP2D { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}