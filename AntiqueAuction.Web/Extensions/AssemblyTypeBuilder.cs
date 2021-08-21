using System;
using System.Collections.Generic;
using System.Linq;

namespace AntiqueAuction.Web.Extensions
{
    public class AssemblyTypesBuilder
    {
        /// <summary>
        /// Give all the executing custom assemblies, excluding builtin or third party assemblies
        /// </summary>
        /// <param name="explicitType"></param>
        /// <returns></returns>
        public static Type[] GetAllExecutingContextTypes(params Type[] explicitType)
        {
            var types = new List<Type>();
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies();

            types.AddRange(GetCommonExecutingContextTypes(assemblies));

   
            return types.ToArray();
        }

        public static Type[] GetCommonExecutingContextTypes(System.Reflection.Assembly[] assemblies) =>
            assemblies
                .Where(x =>
                    x?.ManifestModule.Name! == "AntiqueAuction.Web.dll" ||
                    x?.ManifestModule.Name! == "AntiqueAuction.Application.dll" ||
                    x?.ManifestModule.Name! == "AntiqueAuction.Core.dll" ||
                    x?.ManifestModule.Name! == "AntiqueAuction.Infrastructure.dll" ||
                    x?.ManifestModule.Name! == "AntiqueAuction.Shared.dll")
                .SelectMany(x => x?.GetTypes())
                .ToArray();

    }
}
