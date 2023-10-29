using System.ComponentModel.Composition;

using PNI.Tests.Ioc.MEF.Entities.Implementations;

namespace Schurko.Foundation.Tests.Implementations
{
    [Export(typeof(IDoubleProcessor))]
    public class OverrideDoubleProcessor : IDoubleProcessor
    {
        #region IDoubleProcessor Members

        public double Process(double value)
        {
            return value * value;
        }

        #endregion
    }
}
