﻿using System.ComponentModel.Composition;

using PNI.Foundation.Tests.Entities.Interfaces;

namespace Schurko.Foundation.Tests.Implementations
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