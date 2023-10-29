namespace PNI.Foundation.Tests.Entities.Interfaces
{
    public interface IGearSelector
    {
        int GetBestGear(IGearbox gearbox, int speed);
    }
}
