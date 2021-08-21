namespace AntiqueAuction.Shared.Extensions
{
    public  static class BooleanExtensions
    {
        public static bool ParseBool(this string str, out bool output)
            => bool.TryParse(str, out output);

    }
}
