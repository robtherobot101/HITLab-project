using System.Collections.Generic;
using Grinder;
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

    public abstract class Goal : MonoBehaviour
    {
        [SerializeField] private GameObject screenPrefab;
        [TextArea]
        [SerializeField] private List<string> facilitatorMessage;

        /// <summary>
        ///     A list of resources available to the player.
        /// </summary>
        [SerializeField] private List<ShapeScript> resources;

        [SerializeField] public bool usesShip;

        protected GameObject screen;
        public virtual List<string> FacilitatorMessage => facilitatorMessage;

        public virtual bool FractionLabels => true;


        public virtual List<ShapeScript> Resources => resources;

        /// <summary>
        ///     Whether the goal was achieved and if not, how it failed.
        /// </summary>
        /// <returns>The outcome of the goal.</returns>
        public abstract Outcome GetOutcome();

        /// <summary>
        ///     A description of what should be achieved.
        /// </summary>
        /// <returns>A string describing the objective</returns>
        public abstract string GoalText();

        /// <summary>
        ///     A description of why the objective was (not) achieved.
        /// </summary>
        /// <returns>A string describing why the objective was (not) achieved.</returns>
        public abstract void GiveFeedback();

        public abstract bool RequirementsSatisfied();

        public abstract bool CanAdd();

        public abstract void ShapeAdded(ShapeScript shape);

        public virtual void DisplayScreen()
        {
            screen = ScreenManager.Instance.SetScreen(screenPrefab);
            ScreenRegistered();
        }

        protected abstract void ScreenRegistered();

        public abstract void ClearScreen();
    }
}