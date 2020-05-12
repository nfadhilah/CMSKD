namespace Application.Permission
{
  public class PermissionDto
  {
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string RoleNormalizeName { get; set; }
    public string RoleDescription { get; set; }
    public int MenuId { get; set; }
    public string MenuKode { get; set; }
    public string MenuLabel { get; set; }
    public string MenuUrl { get; set; }
    public string MenuIcon { get; set; }
    public string MenuKdLevel { get; set; }
    public string MenuType { get; set; }
    public bool Maker { get; set; }
    public bool Checker { get; set; }
    public bool Approver { get; set; }
  }
}
