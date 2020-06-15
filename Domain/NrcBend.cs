using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("NRCBEND")]
  public class NrcBend
  {
    [Key, Identity]
    public long IdNrcBend { get; set; }
    public long IdBend { get; set; }
    [LeftJoin("BEND", "IDBEND", "IDBEND")]
    public Bend Bend { get; set; }
    public long IdRek { get; set; }
    [LeftJoin("DAFTREKENING", "IDREK", "IDREK")]
    public DaftRekening DaftRekening { get; set; }
  }
}
