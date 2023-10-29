using PNI.Tests.Ioc.MEF.Entities.Interfaces;

namespace Schurko.Foundation.Tests.Implementations
{
    public class SpaceTaxi : ISpaceship
    {
        #region ISpaceship Members

        public string Name
        {
            get { return "Space Taxi"; }
        }

        #endregion
    }
}
