using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Helpers
{
  public static class ConnectionStringBuilder
  {
    public static string SetDbYear(this IConfiguration config, int? tahun = null, string suffix = null)
    {
      var builder = new SqlConnectionStringBuilder(config.GetConnectionString("Default"));

      if (tahun == null) return builder.ToString();

      // Format Catalog = {Db Nama}_{Db Tahun}_{Db Suffix}
      // eg: V@LID49V6_2018_JABAR

      var existingCatalog = builder.InitialCatalog.Split('_');

      var newCatalogLength = !string.IsNullOrWhiteSpace(suffix) ? existingCatalog.Length + 1 : existingCatalog.Length;

      var newCatalog = new string[newCatalogLength];

      for (var i = 0; i < existingCatalog.Length; i++)
      {
        if (i == 1 && Regex.IsMatch(existingCatalog[i], @"^[12][0-9]{3}$"))
        {
          newCatalog[i] = tahun.Value.ToString();
        }
        else if (i == existingCatalog.Length - 1 && !string.IsNullOrWhiteSpace(suffix))
        {
          newCatalog[i] = suffix;
        }
        else
        {
          newCatalog[i] = existingCatalog[i];
        }
      }

      builder.InitialCatalog = string.Join("_", newCatalog);

      return builder.ToString();
    }
  }
}
