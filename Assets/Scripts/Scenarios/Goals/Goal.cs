﻿﻿using System;
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
        public abstract string HelpText();
    }
}