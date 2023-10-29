﻿using System.ComponentModel.Composition;

using PNI.Foundation.Tests.Entities.Interfaces;


namespace Schurko.Foundation.Tests.Implementations
{
    [Export(typeof(IObjectA))]
    public class ObjectA : IObjectA
    {
        public string Name { get; private set; }

        public IObjectB SubObject { get; private set; }

        [ImportingConstructor]
        public ObjectA(IObjectB subObject)
        {
            Name = "Parent";
            SubObject = subObject;
        }
    }
}