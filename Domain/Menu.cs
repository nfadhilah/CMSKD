﻿using MicroOrm.Dapper.Repositories.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
  [Table("MENU")]
  public class Menu
  {
    [Key, Identity]
    public int Id { get; set; }
    public string Kode { get; set; }
    public string Label { get; set; }
    public string Url { get; set; }
    public string Icon { get; set; }
    public int KdLevel { get; set; }
    public string Type { get; set; }
  }
}
