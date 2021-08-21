namespace AntiqueAuction.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static T SetProperty<T>(this T obj, string property, dynamic value)
        {
            obj.GetType().BaseType?.GetProperty(property)?
                .SetValue(obj, value, null);
            return obj;
        }
    }
}
