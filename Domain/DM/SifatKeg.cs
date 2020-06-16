using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain.DM
{
    [Table("SIFATKEG")]
    public class SifatKeg
    {
        [Key, Identity]
        public long IdSifatKeg { get; set; }
        public string KdSifat { get; set; }
        public string NmSifat { get; set; }
    }
}
