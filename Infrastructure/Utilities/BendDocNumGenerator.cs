using Application.Interfaces;
using AutoWrapper.Wrappers;
using Dapper;
using Domain.DM;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
  public class BendDocNumGenerator : IBendDocNumGenerator
  {
    private readonly IDbContext _context;

    public BendDocNumGenerator(IDbContext context)
    {
      _context = context;
    }

    public async Task<string> GetBendDocNumber(
      long idUnit, long idBend, string kdSet, int kdStatus, string tableName, string column)
    {
      var unit = _context.DaftUnit.Find(x => x.IdUnit == idUnit);

      if (unit == null)
        throw new ApiException(
          $"Incorrect {nameof(idUnit).ToUpper()} Parameter Values");

      var bend = _context.Bend.Find(x => x.IdBend == idBend);

      if (bend == null)
        throw new ApiException(
          $"Incorrect {nameof(idBend).ToUpper()} Parameter Values");

      var webSet = _context.WebSet.Find(x => x.KdSet == kdSet);

      if (webSet == null)
        throw new ApiException(
          $"Incorrect {nameof(webSet).ToUpper()} Parameter Values");

      var statTrs = _context.StatTrs.FindById(kdStatus);

      if (statTrs == null)
        throw new ApiException(
          $"Incorrect {nameof(statTrs).ToUpper()} Parameter Values");

      var values = Regex.Matches(webSet.ValSet, @"\[(.*?)\]")
        .Select(s => s.Groups[1]);

      var formatDict = BuildFormatDict(values, unit, bend, statTrs);

      var generatedNum = await GetLastNumber(idUnit, column, formatDict, tableName, webSet);

      foreach (var (_, (key, value)) in formatDict.Where(f =>
        f.Key != "Number"))
      {
        generatedNum = generatedNum.Replace($"[{key}]", $"{value}");
      }

      return generatedNum;
    }

    public async Task<string> GetRegNumber(
      long idUnit, string tableName, string orderColumnName = "NOREG", int padding = 5)
    {
      var cmd = $@"SELECT TOP (1) CAST(t.NOREG AS INT) + 1
FROM dbo.{tableName} t
WHERE t.IDUNIT = @IdUnit
ORDER BY t.NOREG DESC
";

      var lastNumber = await _context.Connection.QuerySingleOrDefaultAsync<int?>(cmd, new { IdUnit = idUnit }) ?? 1;

      return lastNumber.ToString().PadLeft(padding, '0'); ;
    }

    private static Dictionary<string, KeyValuePair<string, string>> BuildFormatDict(IEnumerable<Group> values, DaftUnit unit, Bend bend, StatTrs statTrs)
    {
      var formatDict = new Dictionary<string, KeyValuePair<string, string>>();

      foreach (var v in values)
      {
        if (v.Value.StartsWith("N"))
          formatDict["Number"] =
            new KeyValuePair<string, string>(v.Value,
              v.Value.Replace("N", "0"));

        switch (v.Value)
        {
          case "YYYY":
            formatDict["Year"] =
              new KeyValuePair<string, string>(v.Value,
                DateTime.Now.Year.ToString("D"));
            break;
          case "JENIS":
            formatDict["JnsTransaksi"] =
                new KeyValuePair<string, string>(v.Value, statTrs.LblStatus);
            break;
          case "SKPD":
            {
              var kdUnit = unit.KdUnit.Trim();
              formatDict["KdUnit"] =
                new KeyValuePair<string, string>(v.Value,
                  kdUnit.Remove(kdUnit.Length - 1));
              break;
            }
          case "BEND":
            formatDict["JnsBend"] =
              new KeyValuePair<string, string>(v.Value, $"B{bend.JnsBend}");
            break;
          case "MM":
            formatDict["Month"] =
              new KeyValuePair<string, string>(v.Value,
                DateTime.Now.Month.ToString("D2"));
            break;
        }
      }

      return formatDict;
    }

    private async Task<string> GetLastNumber(
      long idUnit, string column, Dictionary<string, KeyValuePair<string, string>> formatDict,
      string tableName, WebSet webSet)
    {
      try
      {
        var lastNumber =
          await _context.Connection.QueryFirstOrDefaultAsync<int?>(
            $@"SELECT TOP (1) CAST(LEFT(t.{column}, {formatDict["Number"].Value.Length}) AS INT) + 1
FROM dbo.{tableName} t
WHERE t.IDUNIT = @unitId
ORDER BY t.{column} DESC;", new { unitId = idUnit }) ?? 1;

        var generatedNum = webSet.ValSet
          .Replace($"[{formatDict["Number"].Key}]",
            string.Format(lastNumber.ToString(formatDict["Number"].Value)));
        return generatedNum;
      }
      catch (Exception)
      {
        throw new ApiException("Invalid Format");
      }
    }
  }
}