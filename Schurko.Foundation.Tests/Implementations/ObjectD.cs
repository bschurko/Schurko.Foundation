using System.ComponentModel.Composition;

using PNI.Foundation.Tests.Entities.Interfaces;

namespace PNI.Foundation.Tests.Entities.Implementations
{
    [Export]
    public class ObjectD : IObjectX
    {
        public string Name { get; private set; }

        public ObjectD()
        {
            Name = "ObjectD";
        }
    }
}