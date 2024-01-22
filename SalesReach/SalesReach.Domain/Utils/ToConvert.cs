namespace SalesReach.Domain.Utils
{
    public static class ToConvert
    {
        #region .: DateTime para String:.
        public static string DateTimeNullable(DateTime? data)
        {
            if (data is not null)
                return string.Format("dd-MM-yyyy HH:mm:ss", data);

            return "-";
        }

        public static string DateTimeToString(DateTime data)
            => string.Format("dd-MM-yyyy HH:mm:ss", data);

        #endregion

        #region .: String para DateTime :.

        public static DateTime? StringNullable(string data)
        {
            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }

        public static DateTime StringToDateTime(string data)
            => DateTime.Parse(data);

        #endregion
    }
}
