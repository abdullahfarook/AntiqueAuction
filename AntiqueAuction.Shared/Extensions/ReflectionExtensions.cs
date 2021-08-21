using System;
using System.Collections.Generic;
using System.Linq;

namespace AntiqueAuction.Shared.Extensions
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetGenericImplementedInterfaces(this Type type,Type generic) => 
            type.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == generic);
    }
}
