using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Scenarios.Goals
{
    public class CompositeGoal : Goal
    {
        [SerializeField] private List<Goal> goals;
        private int _goalIndex;
        public override bool FractionLabels => CurrentGoal.FractionLabels;
        public override List<GameObject> Resources => CurrentGoal.Resources;
        private Goal CurrentGoal => goals[_goalIndex];

        public override Outcome GetOutcome()
        {
            return CurrentGoal.GetOutcome();
        }

        public override string GoalText()
        {
            return CurrentGoal.GoalText();
        }

        public override void GiveFeedback()
        {
            CurrentGoal.GiveFeedback();
        }

        public override bool RequirementsSatisfied()
        {
            return CurrentGoal.RequirementsSatisfied();
        }

        public override bool CanAdd()
        {
            return CurrentGoal.CanAdd();
        }

        public override void ShapeAdded(ShapeScript shape)
        {
            CurrentGoal.ShapeAdded(shape);
            if (CurrentGoal.GetOutcome() != Outcome.Achieved) return;
            _goalIndex++;
            if (_goalIndex >= goals.Count) EventManager.Instance.sunk?.Invoke();
            else GameManager.Instance.SetupGoal();
        }

        protected override void ScreenRegistered()
        {
        }

        public override void ClearScreen()
        {
            CurrentGoal.ClearScreen();
        }

        public override void DisplayScreen()
        {
            CurrentGoal.DisplayScreen();
        }
    }
}