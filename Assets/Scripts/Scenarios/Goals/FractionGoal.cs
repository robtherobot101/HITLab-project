using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
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
        
        private int Numerator => numerator;
        [SerializeField] private int numerator;
        private int Denominator => denominator;
        [SerializeField] private int denominator;

        private Fraction goal;

        private void OnEnable()
        {
            goal = new Fraction(numerator, denominator);
        }

        public override Outcome GetOutcome()
        {
            var count = ShapeManager.Instance.CollectedShapes.Aggregate(new Fraction(0, 1), (current, shape) => current + shape.Fraction);

            if (count > goal) return Outcome.Over;
            if (count < goal) return Outcome.Under;
            return Outcome.Achieved;
        }

        public override string GoalText()
        {
            return $"Make {goal} kg of gunpowder to hit the ship.";
        }

        public override string FeedbackText()
        {
            var fractions = ShapeManager.Instance.CollectedShapes.Select(shape => shape.Fraction).ToList();
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
