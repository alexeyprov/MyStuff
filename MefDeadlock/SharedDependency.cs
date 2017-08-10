using MefDeadlock.Children;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefDeadlock
{
    [Export(typeof(ISharedDependency))]
    internal sealed class SharedDependency : ISharedDependency
    {
        private readonly IChildDependency _alpha;

        [ImportingConstructor]
        public SharedDependency([ImportMany] IEnumerable<Lazy<IChildDependency, IChildDependencyMetadata>> children)
        {
            _alpha = children.Single(p => p.Metadata.ChildName == "Alpha").Value;
        }
    }
}
