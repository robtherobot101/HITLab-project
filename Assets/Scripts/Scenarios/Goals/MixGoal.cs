using System;
using System.Collections.Generic;
using Facilitator;
using Managers;
using TMPro;
using UnityEngine;

namespace Scenarios.Goals
{
    public class MixGoal : Goal
    {
        [SerializeField] private List<Target> targets;

        [SerializeField] private GeometryFeedback FeedbackScreen;
        
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
                s += $"{i + 1}. ";
                s += target.attribute switch
                {
                    Attribute.Faces => $"A shape with {target.Shape.Faces.ToString()} faces.",
                    Attribute.Edges => $"A shape with {target.Shape.Edges.ToString()} edges.",
                    Attribute.Vertices => $"A shape with {target.Shape.Vertices.ToString()} vertices.",
                    Attribute.Name => $"A {target.Shape.ShapeName}.",
                    _ => throw new ArgumentOutOfRangeException()
                };

                s += " \n";
                i++;
            }

            return s;
        }

        public override void GiveFeedback()
        {
            var s = "";

            for (var i = 0; i < targets.Count; i++)
            {
                s += $"{i + 1}. ";
                object goal;
                object count;
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
                    case Attribute.Name:
                        goal = targets[i].Shape.ShapeName;
                        count = GameManager.Instance.GrinderShapes[i].Vertices;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (targets[i].attribute == Attribute.Name)
                {
                    if (goal == count)
                    {
                        s += $"Shape {i + 1} was not a {goal}.";
                    }
                }
                else
                {
                    if (goal == count)
                        s +=
                            $"That's correct. {GameManager.Instance.GrinderShapes[i].ShapeName}s have {goal} {targets[i].attribute.ToString()}.\n";
                    else
                        s +=
                            $"That's incorrect. {GameManager.Instance.GrinderShapes[i].ShapeName}s do not have {goal} {targets[i].attribute.ToString()}.\n";
                }
            }

            var o = Instantiate(FeedbackScreen);
            FacilitatorScript.Instance.Say(o.gameObject);
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
            Vertices,
            Name
        }

        [Serializable]
        private struct Target
        {
            [SerializeField] private ShapeScript shape;
            public Attribute attribute;
            public ShapeScript Shape => shape;
        }
    }
}