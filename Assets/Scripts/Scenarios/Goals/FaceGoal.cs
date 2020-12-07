﻿using System;
using System.Linq;
using Managers;
using UnityEngine;

namespace Scenarios.Goals
{
    [CreateAssetMenu]
    public class FaceGoal : Goal
    {

        [SerializeField] private int goal;

        public override Outcome GetOutcome()
        {
            var count = ShapeManager.Instance.CollectedShapes.Sum(shape => shape.Faces);
            if (count < goal) return Outcome.Under;
            if (count > goal) return Outcome.Over;
            return Outcome.Achieved;
        }

        public override string GoalText()
        {
            var count = ShapeManager.Instance.CollectedShapes.Sum(shape => shape.Faces);
            return $"{count} out of {goal} faces collected.";
        }

        public override string FeedbackText()
        {
            var outcome = GetOutcome();
            switch (outcome)
            {
                case Outcome.Over:
                    return $"You put too many faces in the cannon. ";
                case Outcome.Under:
                    return $"You didn't put enough faces in the cannon. ";
                case Outcome.Achieved:
                    return $"You put the right number of faces in the cannon!";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
