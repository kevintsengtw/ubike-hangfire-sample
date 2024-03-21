using System.Net;
using System.Text.RegularExpressions;

// ReSharper disable MethodOverloadWithOptionalParameter

namespace UBike.Respository.Extensions
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if string is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Returns true if string is null, empty or only whitespaces.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// Determines whether [is valid email address] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(this string s)
        {
            var regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            return regex.IsMatch(s);
        }

        /// <summary>
        /// Determines whether [is valid mobile phone] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns><c>true</c> if [is valid mobile phone] [the specified s]; otherwise, <c>false</c>.</returns>
        public static bool IsValidMobilePhone(this string s)
        {
            var regex = new Regex(@"^09\d{8}$");
            return regex.IsMatch(s);
        }

        /// <summary>
        /// Executes a passed Regular Expression against a string and returns the result of the match
        /// </summary>
        /// <param name="s">the target string</param>
        /// <param name="regEx">a System.String containing a regular expression</param>
        /// <returns>true if the regular expression matches the target string and false if it does not</returns>
        public static bool IsMatch(this string s, string regEx)
        {
            return new Regex(regEx).IsMatch(s);
        }

        /// <summary>
        /// Tests a string to see if it is a number
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNumber(this string strNumber)
        {
            var strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            var strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            return !strNumber.IsMatch("[^0-9.-]") &&
                   !strNumber.IsMatch("[0-9]*[.][0-9]*[.][0-9]*") &&
                   !strNumber.IsMatch("[0-9]*[-][0-9]*[-][0-9]*") &&
                   strNumber.IsMatch("({0})|({1})".ToFormat(strValidRealPattern, strValidIntegerPattern));
        }

        //=========================================================================================

        /// <summary>
        /// Returns html-encoded string.
        /// </summary>
        public static string HtmlEncode(this string s)
        {
            return WebUtility.HtmlEncode(s);
        }

        /// <summary>
        /// Returns html-decoded string.
        /// </summary>
        public static string HtmlDecode(this string s)
        {
            return WebUtility.HtmlDecode(s);
        }

        /// <summary>
        /// Returns url-encoded string.
        /// </summary>
        public static string UrlEncode(this string s)
        {
            // use WebUtility not HttpUtility
            return WebUtility.UrlEncode(s);
        }

        /// <summary>
        /// Returns url-decoded string.
        /// </summary>
        public static string UrlDecode(this string s)
        {
            return WebUtility.UrlDecode(s);
        }

        /// <summary>
        /// Format string.
        /// </summary>
        public static string ToFormat(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// Splits the specified data.
        /// </summary>
        /// <param name="s">The data.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string[] ToSplit(this string s, string separator)
        {
            var nullResult = new string[]
            {
            };

            return s.IsNullOrWhiteSpace()
                ? nullResult
                : s.Split(separator.ToCharArray());
        }

        /// <summary>
        /// Lefts the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string Left(this string s, int length)
        {
            length = Math.Max(length, 0);

            return s.Length > length
                ? s.Substring(0, length)
                : s;
        }

        /// <summary>
        /// Rights the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string Right(this string s, int length)
        {
            length = Math.Max(length, 0);

            return s.Length > length
                ? s.Substring(s.Length - length, length)
                : s;
        }

        /// <summary>
        /// Erases the HTML tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string EraseHtmlTag(this string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            var result = value.Replace("<br>", "\n")
                              .Replace("<br/>", "\n")
                              .Replace("<br />", "\n")
                              .Replace("<BR>", "\n")
                              .Replace("<BR/>", "\n")
                              .Replace("<BR />", "\n")
                              .Replace("&nbsp;", string.Empty);

            var regex = new Regex("\\<[^\\>]*\\>");
            result = regex.Replace(result, string.Empty);

            return result.IsNullOrWhiteSpace() ? string.Empty : result.Trim();
        }

        //=========================================================================================

        /// <summary>
        /// To the bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool ToBool(this string value)
        {
            return value.ToBool(false);
        }

        /// <summary>
        /// Try-parse string to bool, else default value.
        /// </summary>
        public static bool ToBool(this string value, bool defaultValue = false)
        {
            return bool.TryParse(value, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// To the int.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static int ToInt(this string input)
        {
            return input.ToInt(0);
        }

        /// <summary>
        /// To the int.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static int ToInt(this string input, int defaultValue = 0)
        {
            return int.TryParse(input, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// To the int16.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt16(this string value)
        {
            Int16 result = 0;

            if (string.IsNullOrEmpty(value).Equals(false))
            {
                Int16.TryParse(value, out result);
            }

            return result;
        }

        /// <summary>
        /// To the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt32(this string value)
        {
            var result = 0;

            if (string.IsNullOrEmpty(value).Equals(false))
            {
                Int32.TryParse(value, out result);
            }

            return result;
        }

        /// <summary>
        /// To the int64.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static long ToInt64(this string value)
        {
            Int64 result = 0;

            if (string.IsNullOrEmpty(value).Equals(false))
            {
                Int64.TryParse(value, out result);
            }

            if (result.Equals(0)
                && !string.IsNullOrEmpty(value)
                && value.Contains('.'))
            {
                result = Convert.ToInt64(value.ToDouble());
            }

            return result;
        }

        /// <summary>
        /// To the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double ToDouble(this string value)
        {
            double result = 0;
            if (string.IsNullOrWhiteSpace(value).Equals(false))
            {
                double.TryParse(value, out result);
            }

            return result;
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Decimal.</returns>
        public static decimal ToDecimal(this string value)
        {
            decimal result = 0;
            if (string.IsNullOrWhiteSpace(value).Equals(false))
            {
                decimal.TryParse(value, out result);
            }

            return result;
        }

        /// <summary>
        /// To the unique identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Guid ToGuid(this string value)
        {
            var result = Guid.Empty;

            if (string.IsNullOrWhiteSpace(value).Equals(false))
            {
                Guid.TryParse(value, out result);
            }

            return result;
        }

        /// <summary>
        /// Truncate
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            var result = value.Trim().Length > maxLength
                ? value.Trim().Substring(0, maxLength)
                : value.Trim();

            return result;
        }
    }
}