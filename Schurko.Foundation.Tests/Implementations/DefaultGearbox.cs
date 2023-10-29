using System.ComponentModel.Composition;

using PNI.Foundation.Tests.Entities.Interfaces;

using PNI.Ioc.MEF;

namespace PNI.Foundation.Tests.Entities.Implementations
{
    [Export(typeof(IGearbox))]
    [ExportMetadata("Default", "IGearbox")]
    public class DefaultGearbox : IGearbox
    {
        #region IGearbox Members

        public IGearSelector GearSelector
        {
            get
            {
                return DependencyInjector.Resolve<IGearSelector>();
            }
        }

        public int CurrentGear { get; set; }

        public int NumberOfGears
        {
            get
            {
                return 4;
            }
        }
        
        public int SelectGear(int speed)
        {
            CurrentGear = GearSelector.GetBestGear(this, speed);
            return CurrentGear;
        }

        #endregion
    }
}
