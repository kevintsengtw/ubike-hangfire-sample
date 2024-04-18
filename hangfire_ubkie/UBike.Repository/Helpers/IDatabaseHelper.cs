using System.Data;

namespace UBike.Repository.Helpers;

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