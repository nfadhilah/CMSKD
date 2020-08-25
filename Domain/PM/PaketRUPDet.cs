using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DM;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain.PM
{
  [Table("PAKETRUPDET")]
  public class PaketRUPDet
  {
    [Key, Identity]
    public long IdRUPDet { get; set; }

    public long IdRUP { get; set; }
    public string KodeRUP { get; set; }
    public string NmPaket { get; set; }
    public string Lokasi { get; set; }
    public string Volume { get; set; }
    public string UraiPaket { get; set; }
    public long? IdJnsPekerjaan { get; set; }

    [LeftJoin("JPEKERJAAN", "IDJNSPEKERJAAN", "IDJNSPEKERJAAN")]
    public JPekerjaan JnsPekerjaan { get; set; }

    public DateTime? AwalPekerjaan { get; set; }
    public DateTime? AkhirPekerjaan { get; set; }
    public long IdJDana { get; set; }

    [InnerJoin("JDANA", "IDJDANA", "IDJDANA")]
    public JDana JDana { get; set; }

    public long IdPhk3 { get; set; }

    [InnerJoin("DAFTPHK3", "IDPHK3", "IDPHK3")]
    public DaftPhk3 Phk3 { get; set; }

    [UpdatedAt]
    public DateTime? DateCreate { get; set; }

    [UpdatedAt]
    public DateTime? DateUpdate { get; set; }
  }
}