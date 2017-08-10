using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefDeadlock.Children
{
    [Export(typeof(IChildDependency))]
    [ExportMetadata("ChildName", "Delta")]
    internal class DeltaChild : IChildDependency
    {
    }
}
