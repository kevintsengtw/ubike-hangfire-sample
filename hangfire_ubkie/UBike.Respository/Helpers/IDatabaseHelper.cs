using System.Data;

namespace UBike.Respository.Helpers;

/// <summary>
/// Interface IDatabaseHelper
/// </summary>
public interface IDatabaseHelper
{
    /// <summary>
    /// Gets the connection.
    /// </summary>
    /// <returns>IDbConnection.</returns>
    IDbConnection GetConnection();
}