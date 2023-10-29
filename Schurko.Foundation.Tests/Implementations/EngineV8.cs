﻿using System.ComponentModel.Composition;

using PNI.Tests.Ioc.MEF.Entities.Interfaces;

namespace Schurko.Foundation.Tests.Implementations
{
    /// <summary>
    /// The engine v 8.
    /// </summary>
    [Export(typeof(IEngine))]
    public class EngineV8 : IEngine
    {
        /// <summary>
        /// Gets the cylinders.
        /// </summary>
        public int Cylinders
        {
            get
            {
                return 8;
            }
        }
    }
}
