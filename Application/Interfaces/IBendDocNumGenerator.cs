using System.Threading.Tasks;

namespace Application.Interfaces
{
  public interface IBendDocNumGenerator
  {
    Task<string> GetBendDocNumber(
      long idUnit, long idBend, string kdSet, int kdStatus, string tableName,
      string column);

    Task<string> GetRegNumber(
      long idUnit, string tableName, string orderColumnName = "NOREG", int padding = 5);
  }
}
