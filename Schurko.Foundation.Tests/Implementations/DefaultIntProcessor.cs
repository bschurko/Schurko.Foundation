using System.ComponentModel.Composition;

using PNI.Tests.Ioc.MEF.Entities.Interfaces;


namespace PNI.Tests.Ioc.MEF.Entities.Implementations
{
    [Export(typeof(IIntProcessor))]
    [ExportMetadata("Default", "IIntProcessor")]
    public class DefaultIntProcessor: IIntProcessor
    {
        #region IIntProcessor Members

        public int ProcessInt(int inValue)
        {
            return inValue * inValue;
        }

        #endregion
    }
}
