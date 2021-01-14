using System;
using System.Collections.Generic;
using Grinder;
using Managers;
using TMPro;
using UnityEngine;

namespace Scenarios.Goals
{
    public class MixGoal : Goal
    {
        [SerializeField] private List<Target> targets;
        private TMP_Text _screenText;
        public override bool FractionLabels => false;


        public override Outcome GetOutcome()
        {
            var i = 0;
            foreach (var shape in GameManager.Instance.GrinderShapes)
            {
                if (!shape.ShapeName.Equals(targets[i].Shape.ShapeName))
                    return Outcome.NotAchieved;
                i++;
            }

            return Outcome.Achieved;
        }

        public override string GoalText()
        {
            var s = "Put the following shapes into the machine, in the order they are given:\n";
            var i = 0;
            foreach (var target in targets)
            {
                s += $"{i + 1}. A shape with ";
                s += target.attribute switch
                {
                    Attribute.Faces => target.Shape.Faces.ToString(),
                    Attribute.Edges => target.Shape.Edges.ToString(),
                    Attribute.Vertices => target.Shape.Vertices.ToString(),
                    _ => throw new ArgumentOutOfRangeException()
                };

                s += $" {target.attribute.ToString().ToLower()}\n";
                i++;
            }

            return s;
        }

        public override string FeedbackText()
        {
            var s = "";

            for (var i = 0; i < targets.Count; i++)
            {
                s += $"{i + 1}. ";
                int goal;
                int count;
                switch (targets[i].attribute)
                {
                    case Attribute.Faces:
                        goal = targets[i].Shape.Faces;
                        count = GameManager.Instance.GrinderShapes[i].Faces;
                        break;
                    case Attribute.Edges:
                        goal = targets[i].Shape.Edges;
                        count = GameManager.Instance.GrinderShapes[i].Edges;
                        break;
                    case Attribute.Vertices:
                        goal = targets[i].Shape.Vertices;
                        count = GameManager.Instance.GrinderShapes[i].Vertices;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (goal == count)
                    s +=
                        $"That's correct. {GameManager.Instance.GrinderShapes[i].ShapeName}s have {goal} {targets[i].attribute.ToString()}.\n";
                else
                    s +=
                        $"That's incorrect. {GameManager.Instance.GrinderShapes[i].ShapeName}s do not have {goal} {targets[i].attribute.ToString()}.\n";
            }

            return s;
        }

        public override bool RequirementsSatisfied()
        {
            return GameManager.Instance.GrinderShapes.Count >= targets.Count;
        }

        public override bool CanAdd()
        {
            return GameManager.Instance.GrinderShapes.Count < targets.Count;
        }

        public override void ShapeAdded(ShapeScript shape)
        {
            _screenText.text += $"{GameManager.Instance.GrinderShapes.Count}. {shape.ShapeName}\n";
        }

        protected override void ScreenRegistered()
        {
            _screenText = screen.GetComponentInChildren<TMP_Text>();
        }

        public override void ClearScreen()
        {
            _screenText.text = "";
        }

        private enum Attribute
        {
            Faces,
            Edges,
            Vertices
        }

        [Serializable]
        private struct Target
        {
            [SerializeField] private GameObject shape;
            public Attribute attribute;
            public ShapeScript Shape => shape.GetComponent<ShapeScript>();
        }
    }
}