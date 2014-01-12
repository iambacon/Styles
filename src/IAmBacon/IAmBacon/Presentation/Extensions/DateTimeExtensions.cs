namespace IAmBacon.Presentation.Extensions
{
    using System;

    /// <summary>
    /// Date time extension methods.
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Creates a date time formatted correctly for the HTML time element.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>A string formatted to date time.</returns>
        public static string ToDateTimeFormat(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        /// <summary>
        /// Creates a display date time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static string ToDisplayDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd H:mm");
        }

        #endregion
    }
}