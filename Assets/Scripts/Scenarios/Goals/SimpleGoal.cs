using System.Collections;
using Grinder;
using Managers;
using TMPro;
using UnityEngine;

namespace Scenarios.Goals
{
    public class SimpleGoal : Goal
    {

        [SerializeField] private string instructions;
        [SerializeField] private string machineMessage;
        [SerializeField] private ShapeScript goalShape;
        [SerializeField] private bool showFractions;

        private TMP_Text _machineText;
        private ShapeScript _givenShape;
        
        public override bool FractionLabels => showFractions;
        
        public override Outcome GetOutcome()
        {
            if (_givenShape != null && _givenShape.ShapeName.Equals(goalShape.ShapeName)) return Outcome.Achieved;
            return Outcome.NotAchieved;
        }

        public override string GoalText()
        {
            return instructions;
        }

        public override string FeedbackText()
        {
            return "Here is some feedback? Not entirely sure what this is supposed to say ¯\\_(ツ)_/¯";
        }

        public override bool RequirementsSatisfied()
        {
            return false;
        }

        public override bool CanAdd()
        {
            return true;
        }

        public override void ShapeAdded(ShapeScript shape)
        {
            _givenShape = shape;
            ScreenManager.Instance.FlashResult(GetOutcome() == Outcome.Achieved);
        }

        protected override void ScreenRegistered()
        {
            _machineText = screen.GetComponentInChildren<TMP_Text>();
            _machineText.alignment = TextAlignmentOptions.Center;
            _machineText.text = machineMessage;
        }

        public override void ClearScreen()
        {
            throw new System.NotImplementedException();
        }
    }
}