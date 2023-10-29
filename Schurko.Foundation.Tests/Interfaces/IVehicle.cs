﻿namespace PNI.Tests.Ioc.MEF.Entities.Interfaces
{
    /// <summary>
    /// The Vehicle interface.
    /// </summary>
    public interface IVehicle
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the engine.
        /// </summary>
        IEngine Engine { get; }
    }
}
