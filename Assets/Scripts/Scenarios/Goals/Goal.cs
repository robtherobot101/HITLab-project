﻿using System;
 using System.Collections.Generic;
 using Grinder;
 using Managers;
 using UnityEngine;

namespace Scenarios.Goals
{

    public enum Outcome
    {
        Over,
        Under,
        NotAchieved,
        Achieved
    }

    public abstract class Goal : ScriptableObject
    {

        [SerializeField] private GameObject screenPrefab;
        protected GameObject screen;

        /// <summary>
        /// A list of resources available to the player.
        /// </summary>
        [SerializeField] private List<GameObject> resources;

        public IEnumerable<GameObject> Resources => resources;

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

        public abstract void ShapeAdded(ShapeScript shape);

        public void DisplayScreen()
        {
            screen = ScreenManager.Instance.SetScreen(screenPrefab);
        }

        public abstract void ClearScreen();

    }
}
