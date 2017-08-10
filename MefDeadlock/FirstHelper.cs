using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefDeadlock
{
    [Export(typeof(IHelper))]
    internal sealed class FirstHelper : IHelper
    {
        private readonly ISharedDependency _dependency;
        private readonly AnotherDependency _anotherDependency;

        [ImportingConstructor]
        public FirstHelper([Import] ISharedDependency dependency, [Import] AnotherDependency anotherDependency)
        {
            _dependency = dependency;
            _anotherDependency = anotherDependency;
        }

        #region IHelper Members

        public void Help()
        {
        }

        #endregion
    }
}
