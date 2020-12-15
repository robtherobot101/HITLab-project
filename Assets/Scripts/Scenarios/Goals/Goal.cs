﻿using System;
 using System.Collections.Generic;
 using UnityEngine;

namespace Scenarios.Goals
{

    public enum Outcome
    {
        Over,
        Under,
        Achieved
    }

    public abstract class Goal : ScriptableObject
    {
        /// <summary>
        /// Whether the goal was achieved and if not, how it failed.
        /// </summary>
        /// <returns>The outcome of the goal.</returns>
        public abstract Outcome GetOutcome();
        
        /// <summary>
        /// A description of what should be achieved.
        /// </summary>
        /// <returns>A string describing the objective</returns>
        public abstract string GoalText();

        /// <summary>
        /// A description of why the objective was (not) achieved.
        /// </summary>
        /// <returns>A string describing why the objective was (not) achieved.</returns>
        public abstract string FeedbackText();

        public abstract bool RequirementsSatisfied();
        
        public abstract bool CanAdd();
        
        /// <summary>
        /// A list of resources available to the player.
        /// </summary>
        [SerializeField] private List<GameObject> resources;

        public IEnumerable<GameObject> Resources => resources;
    }
}
