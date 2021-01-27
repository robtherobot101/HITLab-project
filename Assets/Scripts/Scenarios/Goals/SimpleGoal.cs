using System;
using Facilitator;
using Grinder;
using TMPro;
using UnityEngine;

namespace Scenarios.Goals
{
    public class SimpleGoal : Goal
    {
        [SerializeField] private string instructions;
        [SerializeField] private string machineMessage;
        [SerializeField] private ShapeScript goalShape;
        [SerializeField] private bool isTutorial;

        [SerializeField] private bool showFractions;
        private ShapeScript _givenShape;

        private TMP_Text _machineText;
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

        public override void GiveFeedback()
        {
            FacilitatorScript.Instance.Say(
                "Whoops, that was not the correct shape, try again.");
        }

        public override bool RequirementsSatisfied()
        {
            return isTutorial && GetOutcome() == Outcome.Achieved || !isTutorial;
        }

        public override bool CanAdd()
        {
            return _givenShape == null;
        }

        public override void ShapeAdded(ShapeScript shape)
        {

            if (isTutorial)
            {
                if (shape.ShapeName.Equals(goalShape.ShapeName))
                {
                    ScreenManager.Instance.FlashResult(true);
                    _givenShape = shape;
                }
                else
                {
                    ScreenManager.Instance.FlashResult(false);
                    return;
                }
            }

            else
            {
                _givenShape = shape;
            }
            
            _machineText.text = "Now turn the handle ->";
            
        }

        protected override void ScreenRegistered()
        {
            _machineText = screen.GetComponentInChildren<TMP_Text>();
            _machineText.alignment = TextAlignmentOptions.Center;
            _machineText.text = machineMessage;
        }

        public override void ClearScreen()
        {
            _givenShape = null;
            _machineText.text = machineMessage;
        }
    }
}