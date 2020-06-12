using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;

namespace Domain
{
    [Table("GOLONGAN")]
    public class Golongan
    {
        [Key]
        public string KdGol { get; set; }
        [Key, Identity]
        public long IdGol { get; set; }
        public string NmGol { get; set; }
        public string Pangkat { get; set; }
        [LeftJoin("PEGAWAI", "KDGOL", "KDGOL")]
        public Pegawai Pegawai { get; set; }
    }
}
