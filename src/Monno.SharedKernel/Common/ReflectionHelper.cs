using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Monno.SharedKernel.Common;

public static class ReflectionHelper
{
    public static object? PopulateObject(string assembly, string payload)
    {
        var type = Type.GetType(assembly);

        if (type is null)
            throw new ArgumentException($"Type {assembly} not found");

        var data = JsonConvert.DeserializeObject<JObject>(payload);

        var instance = Activator.CreateInstance(type);

        foreach (var item in data)
        {
            var property = type.GetProperty(item.Key);
            if (property != null && property.CanWrite)
            {
                property.SetValue(instance, Convert.ChangeType(item.Value, property.PropertyType), null);
            }
        }

        return instance;
    }
}