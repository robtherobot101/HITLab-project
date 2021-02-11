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

        [SerializeField] private GeometryFeedback feedbackScreen;

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
            var o = Instantiate(feedbackScreen);
            var feedback = o.GetComponent<GeometryFeedback>();
            FacilitatorScript.Instance.Say(o.gameObject);

            for (var i = 0; i < targets.Count; i++)
            {
                switch (targets[i].attribute)
                {
                    case Attribute.Faces:
                        if (targets[i].Shape.Faces != GameManager.Instance.GrinderShapes[i].Faces)
                        {
                            feedback.FacesWrong();
                        }
                        break;
                    case Attribute.Edges:
                        if (targets[i].Shape.Edges != GameManager.Instance.GrinderShapes[i].Edges)
                        {
                            feedback.EdgesWrong();
                        }
                        break;
                    case Attribute.Vertices:
                        if (targets[i].Shape.Vertices != GameManager.Instance.GrinderShapes[i].Vertices)
                        {
                            feedback.VerticesWrong();
                        }
                        break;
                    case Attribute.Name:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
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