﻿using System;
using Utils;

namespace Managers
{
    public class EventManager : MonoSingleton<EventManager>
    {
        public Action cannonFired;
        // /// <summary>
        // ///     Triggered when a cannon shot misses
        // /// </summary>
        // public Action missed;

        // /// <summary>
        // /// Triggered when a shape is given to the cannon
        // /// </summary>
        // public Action<ShapeScript> componentAdded;

        /// <summary>
        ///     Triggered when the next scenario is loaded
        /// </summary>
        public Action reset;

        /// <summary>
        ///     Triggered after the ship has completely sunk
        /// </summary>
        public Action sunk;
    }
}