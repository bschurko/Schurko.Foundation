namespace PNI.Foundation.Tests.Entities.Interfaces
{
    public interface IObjectA
    {
        string Name { get; }

        IObjectB SubObject { get; } 
    }
}