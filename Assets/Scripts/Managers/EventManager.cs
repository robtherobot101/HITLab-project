using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoSingleton<EventManager>
{
    /// <summary>
    /// Triggered when a cannon shot misses
    /// </summary>
    public Action missed;
    
    /// <summary>
    /// Triggered after the ship has completely sunk
    /// </summary>
    public Action sunk;
    
    // /// <summary>
    // /// Triggered when a shape is given to the cannon
    // /// </summary>
    // public Action<ShapeScript> componentAdded;
    
    /// <summary>
    /// Triggered when the next scenario is loaded
    /// </summary>
    public Action reset;
}
