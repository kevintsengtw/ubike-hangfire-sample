using Dapper;

namespace UBike.Repository.Extensions;

/// <summary>
/// class DapperExtension.
/// </summary>
/// <remarks>
/// 軟體主廚的程式料理廚房 - [料理佳餚] Dapper 用起來很友善，但是預設的參數型別對執行計劃不太友善。
/// https://dotblogs.com.tw/supershowwei/2019/08/12/232213
/// </remarks>
public static class DapperExtension
{
    /// <summary>
    /// Length of the string is default 4000
    /// </summary>
    public static DbString ToVarchar(this string value)
    {
        return new DbString { Value = value, IsAnsi = true };
    }

    /// <summary>
    /// Length of the string -1 for max
    /// </summary>
    public static DbString ToVarchar(this string value, int length)
    {
        return new DbString { Value = value, Length = length, IsAnsi = true };
    }

    /// <summary>
    /// Length of the string is default 4000
    /// </summary>
    public static DbString ToChar(this string value)
    {
        return new DbString { Value = value, IsAnsi = true, IsFixedLength = true };
    }

    /// <summary>
    /// Length of the string -1 for max
    /// </summary>
    public static DbString ToChar(this string value, int length)
    {
        return new DbString { Value = value, Length = length, IsAnsi = true, IsFixedLength = true };
    }

    /// <summary>
    /// Length of the string is default 4000
    /// </summary>
    public static DbString ToNVarchar(this string value)
    {
        return new DbString { Value = value };
    }

    /// <summary>
    /// Length of the string -1 for max
    /// </summary>
    public static DbString ToNVarchar(this string value, int length)
    {
        return new DbString { Value = value, Length = length };
    }

    /// <summary>
    /// Length of the string is default 4000
    /// </summary>
    public static DbString ToNChar(this string value)
    {
        return new DbString { Value = value, IsFixedLength = true };
    }

    /// <summary>
    /// Length of the string -1 for max
    /// </summary>
    public static DbString ToNChar(this string value, int length)
    {
        return new DbString { Value = value, Length = length, IsFixedLength = true };
    }
}