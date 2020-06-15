using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
    [Table("STATTRS")]
    public class StatTrs
    {
        [Key]
        public string KdStatus { get; set; }
        [Key, Identity]
        public long IdStatTrs { get; set; }
        public string LblStatus { get; set; }
        public string Uraian { get; set; }
    }
}
