using System.Reflection;

using AutoMapper;

namespace MyApp.Application.Common.Mappings;

public class MappingProfile
    : Profile
{

    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }



    #region Helper Methods

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
                    .Where(t => t.GetInterfaces().Any(i =>
                        i.IsGenericType
                        && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)));

        foreach (var type in types)
        {
            var method = type.GetMethod("Mapping")
                         ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");
            var instance = Activator.CreateInstance(type);

            method?.Invoke(instance, new object[] { this });
            // method?.Invoke(instance, [this]);
        }
    }

    #endregion
}
