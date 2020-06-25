using MicroOrm.Dapper.Repositories.Attributes;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("PEGAWAI")]
  public class Pegawai
  {
    [Key, Identity]
    public long IdPeg { get; set; }
    public long NIP { get; set; }
    public long IdUnit { get; set; }
    [LeftJoin("DAFTUNIT", "IDUNIT", "IDUNIT")]
    public DaftUnit DaftUnit { get; set; }
    public string KdGol { get; set; }
    [LeftJoin("GOLONGAN", "KDGOL", "KDGOL")]
    public Golongan Golongan { get; set; }
    public string Nama { get; set; }
    public string Alamat { get; set; }
    public string Jabatan { get; set; }
    public string PDDK { get; set; }
    public string NPWP { get; set; }
    public int StAktif { get; set; }
    public DateTime? DateCreate { get; set; }
  }
}