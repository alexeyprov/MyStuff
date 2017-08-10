using MefDeadlock.Children;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefDeadlock
{
    [Export]
    internal sealed class AnotherDependency
    {
        private readonly IChildDependency _delta;

        [ImportingConstructor]
        public AnotherDependency([ImportMany] IEnumerable<Lazy<IChildDependency, IChildDependencyMetadata>> children)
        {
            _delta = children.Single(p => p.Metadata.ChildName == "Delta").Value;
        }
    }
}
