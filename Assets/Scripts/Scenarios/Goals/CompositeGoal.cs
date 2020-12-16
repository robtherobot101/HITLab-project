// using System;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
//
// namespace Scenarios.Goals
// {
//     public class CompositeGoal : Goal
//     {
//         
//         private IEnumerable<Goal> _members = new List<Goal>();
//         
//         public override Outcome GetOutcome()
//         {
//             foreach (var member in _members)
//             {
//                 if (member.GetOutcome() != Outcome.Achieved) return member.GetOutcome();
//             }
//             return Outcome.Achieved;
//         }
//
//         public override string GoalText()
//         {
//             return _members.Aggregate("", (current, member) => current + (member.GoalText() + '\n'));
//         }
//
//         public override string FeedbackText()
//         {
//             return _members.Aggregate("", (current, member) => current + (member.FeedbackText() + '\n'));
//         }
//
//         public override bool RequirementsSatisfied()
//         {
//             return _members.All(member => member.RequirementsSatisfied());
//         }
//
//         public override bool CanAdd()
//         {
//             return _members.All(member => member.CanAdd());
//         }
//
//         public override void ShapeAdded(ShapeScript shape)
//         {
//             throw new NotImplementedException();
//         }
//
//         public override void ClearScreen()
//         {
//             throw new NotImplementedException();
//         }
//     }
// }

