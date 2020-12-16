// ﻿using System;
// using System.Linq;
// using Managers;
// using UnityEngine;
//
// namespace Scenarios.Goals
// {
//     [CreateAssetMenu]
//     public class CountGoal : Goal
//     {
//
//         [SerializeField] private GameObject shape;
//         private string _shapeName;
//         [SerializeField] private int goal;
//
//         private void OnEnable()
//         {
//             _shapeName = shape.GetComponent<ShapeScript>().ShapeName;
//         }
//
//         public override Outcome GetOutcome()
//         {
//             var count = ShapeManager.Instance.CollectedShapes.Count(s => s.ShapeName.Equals(_shapeName));
//             if (count < goal) return Outcome.Under;
//             if (count > goal) return Outcome.Over;
//             return Outcome.Achieved;
//         }
//
//         public override string GoalText()
//         {
//             var count = ShapeManager.Instance.CollectedShapes.Count(s => s.ShapeName.Equals(_shapeName));
//             return $"{count} out of {goal} {_shapeName.ToLower()}s collected.";
//         }
//
//         public override string FeedbackText()
//         {
//             var outcome = GetOutcome();
//             switch (outcome)
//             {
//                 case Outcome.Over:
//                     return $"You put too many {_shapeName.ToLower()}s in the cannon. ";
//                 case Outcome.Under:
//                     return $"You didn't put enough {_shapeName.ToLower()}s in the cannon. ";
//                 case Outcome.Achieved:
//                     return $"You put the right number of {_shapeName.ToLower()}s in the cannon!";
//                 default:
//                     throw new ArgumentOutOfRangeException();
//             }
//         }
//     }
// }

