﻿﻿using System.Collections.Generic;
using Scenarios;
using Scenarios.Goals;
using UnityEngine;
 using Utils;

 namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private List<Scenario> scenarios = new List<Scenario>();
        private List<Scenario>.Enumerator _scenarioEnumerator;

        private void Start()
        {
            EventManager.Instance.missed += GiveFeedback;
            EventManager.Instance.sunk += NextScenario;
            ShapeManager.Instance.onChanged += UpdateInstructions;

            _scenarioEnumerator = scenarios.GetEnumerator();
            if (_scenarioEnumerator.MoveNext()) UpdateInstructions();
        }

        private void UpdateInstructions()
        {
            var s = "";
            foreach (var goal in _scenarioEnumerator.Current.Goals)
            {
                s += "•" + goal.GoalText() + '\n';
            }
            InstructionsManager.Instance.SetText(s);
        }

        public Outcome GoalsOutcome()
        {
            foreach (var goal in _scenarioEnumerator.Current.Goals)
            {
                if (goal.GetOutcome() != Outcome.Achieved) return goal.GetOutcome();
            }
            // return achieved only if each goal is individually achieved
            return Outcome.Achieved;
        }

        private void GiveFeedback()
        {
            var feedbackText = "";
            foreach (var goal in _scenarioEnumerator.Current.Goals)
            {
                feedbackText += goal.HelpText() + "\n";
            }
            FacilitatorScript.Instance.Say(feedbackText);
        }

        private void NextScenario()
        {
            if (_scenarioEnumerator.MoveNext())
            {
                FacilitatorScript.Instance.Say("You completed the task!\nUh-oh, here comes another.");
                EventManager.Instance.reset?.Invoke();
            }
            else FacilitatorScript.Instance.Say("Congratulations! You finished the game.");
        }
    }
}
