using Domain.Auth;
using MicroOrm.Dapper.Repositories.Attributes.Joins;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DM
{
  [Table("DOCSIGNLOG")]
  public class DocSignLog
  {
    [Key]
    public int DocMetaId { get; set; }
    [InnerJoin("DOCMETA", "DOCMETAID", "ID")]
    public DocMeta DocMeta { get; set; }
    [Key]
    public string SignedBy { get; set; }

    [InnerJoin("WEBUSER", "SIGNEDBY", "USERID")]
    public WebUser WebUser { get; set; }
    public DateTime? SignDate { get; set; }
  }
}