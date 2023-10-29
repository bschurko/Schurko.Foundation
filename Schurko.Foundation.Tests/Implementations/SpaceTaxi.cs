using PNI.Tests.Ioc.MEF.Entities.Interfaces;

namespace PNI.Tests.Ioc.MEF.Entities.Implementations
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
