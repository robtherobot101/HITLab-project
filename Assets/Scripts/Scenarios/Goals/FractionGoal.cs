using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Serialization;
using Utils;

namespace Scenarios.Goals
{
    [CreateAssetMenu]
    public class FractionGoal : Goal
    {
        private enum ExpressedAs
        {
            Fraction,
            Decimal
        }

        [SerializeField] private ExpressedAs goalExpression;

        [SerializeField] private Fraction goal;

        private int minTerms = 2;
        private int maxTerms = 2;

        public override Outcome GetOutcome()
        {
            var count = GameManager.Instance.grinderShapes.Aggregate(new Fraction(0, 1), (current, shape) => current + shape.Fraction);

            if (count > goal) return Outcome.Over;
            if (count < goal) return Outcome.Under;
            return Outcome.Achieved;
        }

        public override bool RequirementsSatisfied() => GameManager.Instance.grinderShapes.Count() >= minTerms;
        public override bool CanAdd() => GameManager.Instance.grinderShapes.Count() < maxTerms;
        public override void UpdateScreen()
        {
            throw new NotImplementedException();
        }

        public override string GoalText()
        {
            return $"Make {goal} kg of gunpowder to hit the ship.";
        }

        public override string FeedbackText()
        {
            var fractions = GameManager.Instance.grinderShapes.Select(shape => shape.Fraction).ToList();
            var s = string.Join(" + ", fractions);
            switch (GetOutcome())
            {
                case Outcome.Over:
                    return $"{s} is more than {goal}. Try using fewer objects or smaller fractions.";
                case Outcome.Under:
                    if (s.Equals("")) s = "0";
                    return $"{s} is less than {goal}. Try using more objects or bigger fractions.";
                case Outcome.Achieved:
                    return $"That's correct. {s} = {goal}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
