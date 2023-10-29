﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using PNI.Tests.Ioc.MEF.Entities.Interfaces;

namespace PNI.Tests.Ioc.MEF.Entities.Implementations
{
    [Export(typeof(IVehicle))]
    public class VehicleFerarri : IVehicle
    {
        public string Name { get; set; }

        [Import]
        public IEngine Engine { get; set; }
    }
}