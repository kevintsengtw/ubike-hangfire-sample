using Dapper;
using Throw;
using UBike.Common.Misc;
using UBike.Repository.Extensions;
using UBike.Repository.Helpers;
using UBike.Repository.Interface;
using UBike.Repository.Models;

// ReSharper disable PossibleMultipleEnumeration

namespace UBike.Repository.Implement;

/// <summary>
/// class StationRepository
/// </summary>
/// <seealso cref="IStationRepository" />
public class StationRepository : IStationRepository
{
    private readonly IDatabaseHelper _databaseHelper;

    /// <summary>
    /// Initializes a new instance of the <see cref="StationRepository"/> class
    /// </summary>
    /// <param name="databaseHelper">The database helper</param>
    public StationRepository(IDatabaseHelper databaseHelper)
    {
        this._databaseHelper = databaseHelper;
    }

    /// <summary>
    /// 指定場站代號的資料是否存在.
    /// </summary>
    /// <param name="stationNo">場站代號</param>
    /// <returns>
    ///   <c>true</c> if the specified station no is exists; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> IsExistsAsync(string stationNo)
    {
        stationNo.Throw().IfNullOrWhiteSpace(x => x);

        using var conn = this._databaseHelper.GetConnection();
        const string sqlCommand = """
                                  SELECT
                                    CASE
                                        WHEN EXISTS (SELECT
                                           1
                                          FROM [YoubikeStation] p
                                          WHERE p.StationNo = @StationNo) THEN 1
                                      ELSE 0
                                    END
                                  """;

        var parameters = new DynamicParameters();
        parameters.Add("StationNo", stationNo);

        var query = await conn.QueryFirstOrDefaultAsync<int>(sql: sqlCommand, param: parameters);

        var result = query > 0;
        return result;
    }

    /// <summary>
    /// 依據場站代號取得 Station 資料.
    /// </summary>
    /// <param name="stationNo">場站代號</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">stationNo</exception>
    public async Task<StationModel> GetAsync(string stationNo)
    {
        stationNo.Throw().IfNullOrWhiteSpace(x => x);

        using var conn = this._databaseHelper.GetConnection();

        const string sqlCommand = """
                                  SELECT
                                  	[Id]
                                     ,[StationNo]
                                     ,[StationName]
                                     ,[Total]
                                     ,[StationBikes]
                                     ,[StationArea]
                                     ,[ModifyTime]
                                     ,[Latitude]
                                     ,[Longitude]
                                     ,[Address]
                                     ,[StationAreaEnglish]
                                     ,[StationNameEnglish]
                                     ,[AddressEnglish]
                                     ,[BikeEmpty]
                                     ,[Active]
                                  FROM [dbo].[YoubikeStation]
                                  WHERE [StationNo] = @StationNo
                                  """;

        var parameters = new DynamicParameters();
        parameters.Add("StationNo", stationNo.Truncate(10).ToNVarchar());

        var query = await conn.QueryFirstOrDefaultAsync<StationModel>(sql: sqlCommand, param: parameters);

        return query;
    }

    /// <summary>
    /// 取得指定範圍的 Station 資料.
    /// </summary>
    /// <param name="from">from</param>
    /// <param name="size">size</param>
    /// <returns></returns>
    public async Task<IEnumerable<StationModel>> GetRangeAsync(int from, int size)
    {
        from.Throw().IfLessThanOrEqualTo(0);
        size.Throw().IfLessThanOrEqualTo(0);

        const string sqlCommand = """
                                  SELECT
                                  	[Id]
                                     ,[StationNo]
                                     ,[StationName]
                                     ,[Total]
                                     ,[StationBikes]
                                     ,[StationArea]
                                     ,[ModifyTime]
                                     ,[Latitude]
                                     ,[Longitude]
                                     ,[Address]
                                     ,[StationAreaEnglish]
                                     ,[StationNameEnglish]
                                     ,[AddressEnglish]
                                     ,[BikeEmpty]
                                     ,[Active]
                                  FROM [dbo].[YoubikeStation]
                                  ORDER BY StationNo ASC
                                    OFFSET @OFFSET ROWS
                                    FETCH NEXT @FETCH ROWS only;
                                  """;

        var pageSize = size is < 0 or > 100 ? 100 : size;
        var start = from <= 0 ? 1 : from;

        using var conn = this._databaseHelper.GetConnection();
        var parameters = new DynamicParameters();

        parameters.Add("OFFSET", start - 1);
        parameters.Add("FETCH", pageSize);

        var query = await conn.QueryAsync<StationModel>(sql: sqlCommand, param: parameters);

        var models = query.Any() ? query.ToList() : Enumerable.Empty<StationModel>();
        return models;
    }

    /// <summary>
    /// 取得全部 station 資料數量.
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetTotalCountAsync()
    {
        const string sqlCommand = " SELECT count(p.Id) FROM [YoubikeStation] p WITH (NOLOCK) ";

        using var conn = this._databaseHelper.GetConnection();
        var queryResult = await conn.QueryFirstOrDefaultAsync<int>(sql: sqlCommand);
        return queryResult;
    }

    /// <summary>
    /// 新增 station 資料.
    /// </summary>
    /// <param name="model">Station</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">model</exception>
    public async Task<IResult> InsertAsync(StationModel model)
    {
        model.ThrowIfNull();

        using var conn = this._databaseHelper.GetConnection();

        const string sqlCommand = """
                                  INSERT INTO [Dbo].[YoubikeStation]
                                  (
                                  	[Id]
                                  	,[StationNo]
                                  	,[StationName]
                                  	,[Total]
                                  	,[StationBikes]
                                  	,[StationArea]
                                  	,[ModifyTime]
                                  	,[Latitude]
                                  	,[Longitude]
                                  	,[Address]
                                  	,[StationAreaEnglish]
                                  	,[StationNameEnglish]
                                  	,[AddressEnglish]
                                  	,[BikeEmpty]
                                  	,[Active]
                                  )
                                  VALUES
                                  (
                                  	@Id
                                  	,@StationNo
                                  	,@StationName
                                  	,@Total
                                  	,@StationBikes
                                  	,@StationArea
                                  	,@ModifyTime
                                  	,@Latitude
                                  	,@Longitude
                                  	,@Address
                                  	,@StationAreaEnglish
                                  	,@StationNameEnglish
                                  	,@AddressEnglish
                                  	,@BikeEmpty
                                  	,@Active
                                  )
                                  """;

        var parameters = new DynamicParameters();
        parameters.Add("Id", model.Id);
        parameters.Add("StationNo", model.StationNo.Truncate(10).ToNVarchar());
        parameters.Add("StationName", model.StationName.Truncate(50).ToNVarchar());
        parameters.Add("Total", model.Total);
        parameters.Add("StationBikes", model.StationBikes);
        parameters.Add("StationArea", model.StationArea.Truncate(10).ToNVarchar());
        parameters.Add("ModifyTime", model.ModifyTime);
        parameters.Add("Latitude", model.Latitude);
        parameters.Add("Longitude", model.Longitude);
        parameters.Add("Address", model.Address.Truncate(200).ToNVarchar());
        parameters.Add("StationAreaEnglish", model.StationAreaEnglish.Truncate(50).ToNVarchar());
        parameters.Add("StationNameEnglish", model.StationNameEnglish.Truncate(100).ToNVarchar());
        parameters.Add("AddressEnglish", model.AddressEnglish.Truncate(500).ToNVarchar());
        parameters.Add("BikeEmpty", model.BikeEmpty);
        parameters.Add("Active", model.Active);

        var executeResult = await conn.ExecuteAsync(sql: sqlCommand, param: parameters);

        IResult result = new Result(false);

        if (executeResult.Equals(0))
        {
            result.Message = "資料新增失敗";
        }
        else
        {
            result.Success = true;
            result.AffectRows = executeResult;
        }

        return result;
    }

    /// <summary>
    /// 新增多筆 Station 資料.
    /// </summary>
    /// <param name="models">The models.</param>
    /// <returns></returns>
    public async Task<IResult> BulkInsertAsync(IEnumerable<StationModel> models)
    {
        models.ThrowIfNull().IfEmpty();

        const string sqlCommand = """
                                  INSERT INTO [Dbo].[YoubikeStation]
                                  (
                                  	[Id]
                                  	,[StationNo]
                                  	,[StationName]
                                  	,[Total]
                                  	,[StationBikes]
                                  	,[StationArea]
                                  	,[ModifyTime]
                                  	,[Latitude]
                                  	,[Longitude]
                                  	,[Address]
                                  	,[StationAreaEnglish]
                                  	,[StationNameEnglish]
                                  	,[AddressEnglish]
                                  	,[BikeEmpty]
                                  	,[Active]
                                  )
                                  VALUES
                                  (
                                  	@Id
                                  	,@StationNo
                                  	,@StationName
                                  	,@Total
                                  	,@StationBikes
                                  	,@StationArea
                                  	,@ModifyTime
                                  	,@Latitude
                                  	,@Longitude
                                  	,@Address
                                  	,@StationAreaEnglish
                                  	,@StationNameEnglish
                                  	,@AddressEnglish
                                  	,@BikeEmpty
                                  	,@Active
                                  );
                                  """;

        var parametersCollection = new List<DynamicParameters>();

        foreach (var model in models)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", model.Id);
            parameters.Add("StationNo", model.StationNo.Truncate(10).ToNVarchar());
            parameters.Add("StationName", model.StationName.Truncate(50).ToNVarchar());
            parameters.Add("Total", model.Total);
            parameters.Add("StationBikes", model.StationBikes);
            parameters.Add("StationArea", model.StationArea.Truncate(10).ToNVarchar());
            parameters.Add("ModifyTime", model.ModifyTime);
            parameters.Add("Latitude", model.Latitude);
            parameters.Add("Longitude", model.Longitude);
            parameters.Add("Address", model.Address.Truncate(200).ToNVarchar());
            parameters.Add("StationAreaEnglish", model.StationAreaEnglish.Truncate(50).ToNVarchar());
            parameters.Add("StationNameEnglish", model.StationNameEnglish.Truncate(100).ToNVarchar());
            parameters.Add("AddressEnglish", model.AddressEnglish.Truncate(500).ToNVarchar());
            parameters.Add("BikeEmpty", model.BikeEmpty);
            parameters.Add("Active", model.Active);

            parametersCollection.Add(parameters);
        }

        using var conn = this._databaseHelper.GetConnection();
        conn.Open();
        using var trans = conn.BeginTransaction();

        var executeResult = await conn.ExecuteAsync(sql: sqlCommand, param: parametersCollection, transaction: trans);

        IResult result = new Result(false);

        if (executeResult.Equals(0))
        {
            result.Message = "資料新增失敗";
            trans.Rollback();
        }
        else
        {
            result.Success = true;
            result.AffectRows = executeResult;
            trans.Commit();
        }

        return result;
    }

    /// <summary>
    /// 刪除指定的 station 資料.
    /// </summary>
    /// <param name="stationNo">The stationNo.</param>
    /// <returns></returns>
    public async Task<IResult> DeleteAsync(string stationNo)
    {
        stationNo.Throw().IfNullOrWhiteSpace(x => x);

        var exists = await this.IsExistsAsync(stationNo);
        if (!exists)
        {
            return new Result(false) { Message = "資料不存在" };
        }

        const string sqlCommand = """
                                  begin tran
                                  DELETE FROM [dbo].[YoubikeStation]
                                  WHERE StationNo = @StationNo
                                  commit
                                  """;

        var parameters = new DynamicParameters();
        parameters.Add("StationNo", stationNo);

        using var conn = this._databaseHelper.GetConnection();

        var executeResult = await conn.ExecuteAsync(sql: sqlCommand, param: parameters);

        IResult result = new Result(false);

        if (executeResult.Equals(1))
        {
            result.Success = true;
            result.AffectRows = executeResult;
            return result;
        }

        result.Message = "資料刪除錯誤";
        return result;
    }

    /// <summary>
    /// 刪除多筆的 station 資料.
    /// </summary>
    /// <param name="stationNumbers">The stationNumbers.</param>
    /// <returns></returns>
    public async Task<IResult> BulkDeleteAsync(IEnumerable<string> stationNumbers)
    {
        stationNumbers.ThrowIfNull().IfEmpty();

        const string sqlCommand = """
                                  DELETE FROM [dbo].[YoubikeStation]
                                  WHERE StationNo = @StationNo
                                  """;

        var existingStationNumbers = await this.FilterExistingStationNumbersAsync(stationNumbers);
        
        var parametersCollection = new List<DynamicParameters>();

        foreach (var stationNo in existingStationNumbers)
        {
            var parameters = new DynamicParameters();
            parameters.Add("StationNo", stationNo);
            parametersCollection.Add(parameters);
        }

        using var conn = this._databaseHelper.GetConnection();
        conn.Open();
        using var trans = conn.BeginTransaction();

        var executeResult = await conn.ExecuteAsync(sql: sqlCommand, param: parametersCollection, transaction: trans);

        IResult result = new Result(false);

        if (executeResult.Equals(0))
        {
            result.Message = "資料刪除錯誤";
            trans.Rollback();
        }
        else
        {
            result.Success = true;
            result.AffectRows = executeResult;
            trans.Commit();
        }

        return result;
    }

    /// <summary>
    /// 過濾出存在的站點
    /// </summary>
    /// <param name="stationNumbers">stationNumbers</param>
    /// <returns></returns>
    private async Task<HashSet<string>> FilterExistingStationNumbersAsync(IEnumerable<string> stationNumbers)
    {
        const string query = """
                             SELECT StationNo
                             FROM [dbo].[YoubikeStation]
                             WHERE StationNo IN @StationNumbers
                             """;

        using var conn = this._databaseHelper.GetConnection();
        var existingStationNumbers = await conn.QueryAsync<string>(query, new { StationNumbers = stationNumbers });
        return existingStationNumbers.ToHashSet();
    }

    /// <summary>
    /// 以 area 查詢並取得 staion 資料.
    /// </summary>
    /// <param name="area">The area.</param>
    /// <param name="from">From.</param>
    /// <param name="size">The size.</param>
    /// <returns></returns>
    public async Task<IEnumerable<StationModel>> QueryByAreaAsync(string area, int @from, int size)
    {
        area.Throw().IfNullOrWhiteSpace(x => x);
        from.Throw().IfLessThanOrEqualTo(0);
        size.Throw().IfLessThanOrEqualTo(0);

        const string sqlCommand = """
                                  SELECT
                                  	[Id]
                                     ,[StationNo]
                                     ,[StationName]
                                     ,[Total]
                                     ,[StationBikes]
                                     ,[StationArea]
                                     ,[ModifyTime]
                                     ,[Latitude]
                                     ,[Longitude]
                                     ,[Address]
                                     ,[StationAreaEnglish]
                                     ,[StationNameEnglish]
                                     ,[AddressEnglish]
                                     ,[BikeEmpty]
                                     ,[Active]
                                  FROM [dbo].[YoubikeStation]
                                  where StationArea = @StationArea
                                  ORDER BY StationNo ASC
                                    OFFSET @OFFSET ROWS
                                    FETCH NEXT @FETCH ROWS only;
                                  """;

        var pageSize = size is < 0 or > 100 ? 100 : size;
        var start = from <= 0 ? 1 : from;

        var parameters = new DynamicParameters();
        parameters.Add("StationArea", area.Truncate(10).ToNVarchar());
        parameters.Add("OFFSET", start - 1);
        parameters.Add("FETCH", pageSize);

        using var conn = this._databaseHelper.GetConnection();
        var query = await conn.QueryAsync<StationModel>(sql: sqlCommand, param: parameters);
        var models = query.Any() ? query.ToList() : Enumerable.Empty<StationModel>();
        return models;
    }

    /// <summary>
    /// 以 area 查詢並取得 station 資料筆數.
    /// </summary>
    /// <param name="area">The area.</param>
    /// <returns></returns>
    public async Task<int> GetCountByAreaAsync(string area)
    {
        area.Throw().IfNullOrWhiteSpace(x => x);

        const string sqlCommand = """
                                  SELECT count(p.Id) FROM [YoubikeStation] p WITH (NOLOCK)
                                  Where p.StationArea = @StationArea
                                  """;

        var parameters = new DynamicParameters();
        parameters.Add("StationArea", area.Truncate(10).ToNVarchar());

        using var conn = this._databaseHelper.GetConnection();
        var queryResult = await conn.QueryFirstOrDefaultAsync<int>(sql: sqlCommand, param: parameters);
        return queryResult;
    }
}