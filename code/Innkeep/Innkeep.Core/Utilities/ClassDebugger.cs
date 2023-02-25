using System.Collections;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;

namespace Innkeep.Core.Utilities;

public static class ClassDebugger
{
    public static string CreateDebugString<T>(T obj)
    {
        var sb = new StringBuilder();
        
        foreach (var property in typeof(T).GetProperties())
        {
            var value = property.GetValue(obj);

            if (property.PropertyType != typeof(string) && value is IEnumerable enumerable)
            {
                sb.AppendLine($"{property.Name}:");
                
                foreach (var item in enumerable)
                {
                    sb.AppendLine("\t" + item);
                }
                continue;
            }
            
            sb.AppendLine($"{property.Name}: {property.GetValue(obj)}");
        }
        
        sb.AppendLine(new string('-', 50));

        return sb.ToString();
    }
}