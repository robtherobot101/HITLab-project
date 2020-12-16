using System.Collections.Generic;
using System.Linq;
using Grinder;
using Scenarios.Goals;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private List<Goal> goals = new List<Goal>();
        [SerializeField] private GameObject barrelPrefab;
        private readonly List<GameObject> _barrels = new List<GameObject>();
        private List<Goal>.Enumerator _goalEnumerator;

        public IEnumerable<ShapeScript> grinderShapes;
        public Goal CurrentGoal => _goalEnumerator.Current;

        private void Start()
        {
            EventManager.Instance.missed += GiveFeedbackAndReset;
            EventManager.Instance.sunk += NextScenario;
            grinderShapes = GameObject.Find("Grinder").GetComponent<GrinderScript>().Shapes;

            _goalEnumerator = goals.GetEnumerator();
            if (_goalEnumerator.MoveNext())
            {
                GenerateBarrels();
                UpdateInstructions();
                _goalEnumerator.Current.DisplayScreen();
            }
        }

        private void GenerateBarrels()
        {
            foreach (var barrel in _barrels) Destroy(barrel);
            _barrels.Clear();
            var i = 0;

            foreach (var shape in _goalEnumerator.Current.Resources.OrderBy(value => Random.value))
            {
                var barrel = BarrelScript.Create(barrelPrefab,
                    barrelPrefab.transform.position + Vector3.left * (1.5f * i), Quaternion.LookRotation(Vector3.back),
                    shape);
                _barrels.Add(barrel);
                i++;
            }
        }

        private void UpdateInstructions()
        {
            InstructionsManager.Instance.SetText(_goalEnumerator.Current.GoalText());
        }

        public Outcome GoalsOutcome()
        {
            return _goalEnumerator.Current.GetOutcome();
        }

        private void GiveFeedbackAndReset()
        {
            FacilitatorScript.Instance.Say(_goalEnumerator.Current.FeedbackText());
            CurrentGoal.ClearScreen();
            EventManager.Instance.reset?.Invoke();
        }

        private void NextScenario()
        {
            if (_goalEnumerator.MoveNext())
            {
                FacilitatorScript.Instance.Say("You completed the task!\nUh-oh, here comes another.");
                EventManager.Instance.reset?.Invoke();
                GenerateBarrels();
                UpdateInstructions();
                _goalEnumerator.Current.DisplayScreen();
            }
            else
            {
                FacilitatorScript.Instance.Say("Congratulations! You finished the game.");
            }
        }
    }
}