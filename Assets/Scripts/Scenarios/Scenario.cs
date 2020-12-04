using System;
using System.Collections.Generic;
using Scenarios.Goals;
using UnityEngine;

namespace Scenarios
{
    [CreateAssetMenu]
    public class Scenario : ScriptableObject
    {
        /// <summary>
        /// A list of resources available to the player.
        /// </summary>
        [SerializeField] private List<GameObject> resources;
        
        /// <summary>
        /// A list of objectives the player should complete.
        /// </summary>
        [SerializeField] private List<Goal> goals;

        public IEnumerable<Goal> Goals => goals;
    }
}
