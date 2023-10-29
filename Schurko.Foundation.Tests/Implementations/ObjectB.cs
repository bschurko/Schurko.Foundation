using System.ComponentModel.Composition;

using PNI.Foundation.Tests.Entities.Interfaces;

namespace PNI.Foundation.Tests.Entities.Implementations
{
    [Export(typeof(IObjectB))]
    public class ObjectB : IObjectB
    {
        public string Name { get; private set; }

        [Import]
        public IObjectX SubItem1 { get; private set; }

        [Import(typeof(ObjectD))]
        public IObjectX SubItem2 { get; private set; }

        public ObjectB()
        {
            Name = "Child";
        }
    }
}