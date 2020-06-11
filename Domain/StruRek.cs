using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;

namespace Domain
{
    [Table("STRUREK")]
    public class StruRek
    {
        [Key]
        public int MtgLevel { get; set; }
        [Key, Identity]
        public long IdStruRek { get; set; }
        public string NmLevel { get; set; }
    }
}
