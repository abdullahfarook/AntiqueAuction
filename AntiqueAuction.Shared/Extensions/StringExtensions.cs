namespace AntiqueAuction.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmp(this string val)
            => string.IsNullOrEmpty(val);
    }
}
