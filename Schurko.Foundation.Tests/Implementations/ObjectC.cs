using System.ComponentModel.Composition;

using PNI.Foundation.Tests.Entities.Interfaces;

namespace PNI.Foundation.Tests.Entities.Implementations
{
    [Export(typeof(IObjectX))]
    public class ObjectC : IObjectX
    {
        public string Name { get; private set; }

        public ObjectC()
        {
            Name = "ObjectC";
        }
    }
}