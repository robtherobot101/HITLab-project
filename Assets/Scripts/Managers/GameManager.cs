﻿using System.Collections.Generic;
 using System.Linq;
 using Scenarios;
using Scenarios.Goals;
using UnityEngine;
 using Utils;

 namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private List<Scenario> scenarios = new List<Scenario>();
        [SerializeField] private GameObject barrelPrefab;
        private List<Scenario>.Enumerator _scenarioEnumerator;
        private List<GameObject> _barrels = new List<GameObject>();
        
        private void Start()
        {
            EventManager.Instance.missed += GiveFeedbackAndReset;
            EventManager.Instance.sunk += NextScenario;
            ShapeManager.Instance.onChanged += UpdateInstructions;

            _scenarioEnumerator = scenarios.GetEnumerator();
            if (_scenarioEnumerator.MoveNext())
            {
                GenerateBarrels();
                UpdateInstructions();
            }
        }

        private void GenerateBarrels()
        {
            foreach (var barrel in _barrels)
            {
                Destroy(barrel);
            }
            _barrels.Clear();
            var i = 0;
            foreach (var shape in _scenarioEnumerator.Current.Resources)
            {
                Debug.Log(shape.GetComponent<ShapeScript>().Fraction);
                var barrel = BarrelScript.Create(barrelPrefab, barrelPrefab.transform.position + Vector3.left * (1.5f * i), Quaternion.LookRotation(Vector3.back),  shape);
                _barrels.Add(barrel);
                i++;
            }
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

        private void GiveFeedbackAndReset()
        {
            var feedbackText = "";
            foreach (var goal in _scenarioEnumerator.Current.Goals)
            {
                feedbackText += goal.FeedbackText() + "\n";
            }
            FacilitatorScript.Instance.Say(feedbackText);
            ShapeManager.Instance.Clear();
        }

        private void NextScenario()
        {
            if (_scenarioEnumerator.MoveNext())
            {
                FacilitatorScript.Instance.Say("You completed the task!\nUh-oh, here comes another.");
                EventManager.Instance.reset?.Invoke();
                GenerateBarrels();
            }
            else FacilitatorScript.Instance.Say("Congratulations! You finished the game.");
        }
    }
}
