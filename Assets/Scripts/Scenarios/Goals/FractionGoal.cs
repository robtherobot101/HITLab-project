﻿using System;
using System.Linq;
using Managers;
using UnityEngine;
using Utils;

namespace Scenarios.Goals
{
    [CreateAssetMenu]
    public class FractionGoal : Goal
    {
        [SerializeField] private ExpressedAs goalExpression;

        [SerializeField] private Fraction goal;
        private FractionScript[] _fractions;
        private readonly int _maxTerms = 2;

        private readonly int _minTerms = 2;

        public override bool FractionLabels => true;

        public override Outcome GetOutcome()
        {
            var count = GameManager.Instance.grinderShapes.Aggregate(new Fraction(0, 1),
                (current, shape) => current + shape.Fraction);

            if (count > goal) return Outcome.Over;
            if (count < goal) return Outcome.Under;
            return Outcome.Achieved;
        }

        public override bool RequirementsSatisfied()
        {
            return GameManager.Instance.grinderShapes.Count() >= _minTerms;
        }

        public override bool CanAdd()
        {
            return GameManager.Instance.grinderShapes.Count() < _maxTerms;
        }

        // TODO Make this better x10000
        public override void ShapeAdded(ShapeScript shape)
        {
            _fractions[GameManager.Instance.grinderShapes.Count() - 1].SetFraction(shape.Fraction, Color.red);
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

        private enum ExpressedAs
        {
            Fraction,
            Decimal
        }
    }
}