using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefDeadlock
{
    [Export]
    internal sealed class CompositionRoot
    {
        [ImportingConstructor]
        public CompositionRoot(
            [ImportMany]  IEnumerable<Lazy<IHelper, IHelperMetadata>> helpers)
        {
                Task.WaitAll(
                   helpers.Select(h => Task.Run(() => RunHelper(h))).ToArray());
        }

        private static void RunHelper(Lazy<IHelper> helper)
        {
            helper.Value.Help();
            Console.WriteLine(helper.GetType());
        }
    }
}
