using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.MA
{
  [Table("DPAR")]
  public class DPAR
  {
    [Key, Identity]
    public long IdDPAR { get; set; }
    public long IdDPA { get; set; }
    [LeftJoin("DPA", "IDDPA", "IDDPA")]
    public DPA DPA { get; set; }
    public string KdTahap { get; set; }
    public int? IdXKode { get; set; }
    public long IdKeg { get; set; }
    [LeftJoin("KEGUNIT", "IDKEG", "IDKEG")]
    public KegUnit KegUnit { get; set; }
    public long IdRek { get; set; }
    [LeftJoin("DAFTREKENING", "IDREK", "IDREK")]
    public DaftRekening DaftRekening { get; set; }
    public Decimal? Nilai { get; set; }
    [UpdatedAt]
    public DateTime? DateCreate { get; set; }
    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}