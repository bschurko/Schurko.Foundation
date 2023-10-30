﻿using System;

namespace VivaVictoria.Retask.Interfaces
{
    public interface IScheduleSettings
    {
        public TimeSpan MaxDifference => TimeSpan.FromMilliseconds(500);
        public TimeSpan MaxTimeout => TimeSpan.FromMilliseconds(0);
        public bool HideExceptions => false;
    }
}
