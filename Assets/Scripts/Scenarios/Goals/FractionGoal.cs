using System.Linq;
using Facilitator;
using Managers;
using UnityEngine;
using Utils;

namespace Scenarios.Goals
{
    public class FractionGoal : Goal
    {
        [SerializeField] private ExpressedAs goalExpression;
        [SerializeField] private GameObject feedbackPrefab;

        [SerializeField] private Fraction goal;
        private readonly int _maxTerms = 2;

        private readonly int _minTerms = 2;
        private FractionScript[] _fractions;

        public override bool FractionLabels => true;

        public override Outcome GetOutcome()
        {
            var count = GameManager.Instance.GrinderShapes.Aggregate(new Fraction(0, 1),
                (current, shape) => current + shape.Fraction);

            if (count > goal) return Outcome.Over;
            if (count < goal) return Outcome.Under;
            return Outcome.Achieved;
        }

        public override bool RequirementsSatisfied()
        {
            return GameManager.Instance.GrinderShapes.Count >= _minTerms;
        }

        public override bool CanAdd()
        {
            return GameManager.Instance.GrinderShapes.Count < _maxTerms;
        }

        // TODO Make this better x10000
        public override void ShapeAdded(ShapeScript shape)
        {
            _fractions[GameManager.Instance.GrinderShapes.Count - 1].SetFraction(shape.Fraction, Color.red);
        }

        protected override void ScreenRegistered()
        {
            _fractions = screen.GetComponentsInChildren<FractionScript>();
        }

        public override void ClearScreen()
        {
            foreach (var fractionText in _fractions) fractionText.SetFraction(Fraction.Zero, Color.black);
        }

        public override string GoalText()
        {
            return $"Add two fractions to make {goal} kg of gunpowder.";
        }

        public override void GiveFeedback()
        {
            // var fractions = GameManager.Instance.GrinderShapes.Select(shape => shape.Fraction).ToList();
            // var s = string.Join(" + ", fractions);
            // switch (GetOutcome())
            // {
            //     case Outcome.Over:
            //         return $"{s} is more than {goal}. Try using fewer objects or smaller fractions.";
            //     case Outcome.Under:
            //         if (s.Equals("")) s = "0";
            //         return $"{s} is less than {goal}. Try using more objects or bigger fractions.";
            //     case Outcome.Achieved:
            //         return $"That's correct. {s} = {goal}";
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }

            var fractions = GameManager.Instance.GrinderShapes.Select(shape => shape.Fraction).ToList();
            var o = Instantiate(feedbackPrefab);
            o.GetComponent<FractionAdditionExplanation>().Init(fractions[0], fractions[1]);
            FacilitatorScript.Instance.Say(o);
        }

        private enum ExpressedAs
        {
            Fraction,
            Decimal
        }
    }
}