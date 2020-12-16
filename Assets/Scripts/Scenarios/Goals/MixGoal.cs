using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;

namespace Scenarios.Goals
{
    [CreateAssetMenu]
    public class MixGoal : Goal
    {
        [SerializeField] private List<Target> targets;
        private TMP_Text _screenText;

        public override Outcome GetOutcome()
        {
            var i = 0;
            foreach (var shape in GameManager.Instance.grinderShapes)
            {
                if (!shape.ShapeName.Equals(targets[i].shape.GetComponent<ShapeScript>().ShapeName))
                    return Outcome.NotAchieved;
                i++;
            }

            return Outcome.Achieved;
        }

        public override string GoalText()
        {
            var s = "";
            var i = 0;
            foreach (var target in targets)
            {
                s += $"{i + 1}. A shape with ";
                switch (target.attribute)
                {
                    case Attribute.Faces:
                        s += target.shape.GetComponent<ShapeScript>().Faces.ToString();
                        break;
                    case Attribute.Edges:
                        s += target.shape.GetComponent<ShapeScript>().Edges.ToString();
                        break;
                    case Attribute.Vertices:
                        s += target.shape.GetComponent<ShapeScript>().Vertices.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                s += $" {target.attribute.ToString().ToLower()}\n";
                i++;
            }

            return s;
        }

        public override string FeedbackText()
        {
            return "THIS PLACEHOLDER TEXT _WILL_ BE CHANGED";
        }

        public override bool RequirementsSatisfied()
        {
            return GameManager.Instance.grinderShapes.Count() >= targets.Count;
        }

        public override bool CanAdd()
        {
            return GameManager.Instance.grinderShapes.Count() < targets.Count;
        }

        public override void ShapeAdded(ShapeScript shape)
        {
            _screenText.text += $"{GameManager.Instance.grinderShapes.Count()}. {shape.ShapeName}\n";
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
            public GameObject shape;
            public Attribute attribute;
        }
    }
}