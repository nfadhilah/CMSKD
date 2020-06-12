using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
    [Table("STRUUNIT")]
    public class StruUnit
    {
        [Key]
        public int KdLevel { get; set; }
        [Key, Identity]
        public long IdStruUnit { get; set; }
        public string NmLevel { get; set; }
        public string NumDigit { get; set; }
    }
}
