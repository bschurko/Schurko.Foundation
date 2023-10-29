using System;
using System.ComponentModel.Composition;

using PNI.Tests.Ioc.MEF.Entities.Implementations;


namespace PNI.Tests.Ioc.MEF.Entities.Implementations
{
    [Export(typeof(IDoubleProcessor))]
    [ExportMetadata("Default", "IDoubleProcessor")]
    public class DefaultDoubleProcessor : IDoubleProcessor
    {
        #region IDoubleProcessor Members

        public double Process(double value)
        {
            // Should never get called.
            throw new NotImplementedException();
        }

        #endregion
    }
}
