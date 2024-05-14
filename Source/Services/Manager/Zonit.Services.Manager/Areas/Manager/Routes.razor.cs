using Microsoft.AspNetCore.Components;
using System.Reflection;
using Zonit.Extensions.Website.Abstractions.Areas;

namespace Zonit.Services.Manager.Areas.Manager;

public partial class Routes : ComponentBase
{
    public IEnumerable<Assembly> Types() =>
    AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => !string.Equals(a.FullName, "Microsoft.Data.SqlClient, Version=5.0.0.0, Culture=neutral, PublicKeyToken=23ec7fc2d6eaa4a5", StringComparison.OrdinalIgnoreCase))
        .SelectMany(s => s.GetTypes())
        .Where(p => !p.IsAbstract && typeof(IAreaManager).IsAssignableFrom(p))
        .Select(x => x.Assembly)
        .Distinct();

    public static IEnumerable<Assembly> GetAssemblies()
    {
        var loadedAssemblies = new HashSet<string>();
        var queue = new Queue<Assembly>();

        queue.Enqueue(Assembly.GetEntryAssembly());

        while (queue.Count > 0)
        {
            var asm = queue.Dequeue();

            yield return asm;

            foreach (var reference in asm.GetReferencedAssemblies())
            {
                if (!loadedAssemblies.Contains(reference.FullName))
                {
                    var loadedAssembly = Assembly.Load(reference);
                    queue.Enqueue(loadedAssembly);
                    loadedAssemblies.Add(reference.FullName);
                }
            }
        }
    }
}
