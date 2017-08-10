using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MefDeadlock
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            ComposablePartCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

            CompositionContainer container = new CompositionContainer(catalog, true);

            CompositionRoot root = container.GetExportedValue<CompositionRoot>();
        }
    }
}
