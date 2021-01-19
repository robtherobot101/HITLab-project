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
        [SerializeField] private Transform barrelArea;
        [SerializeField] private EnemyScript enemyShip;
        public Vector3 EnemyPosition => enemyShip.transform.position;
        [SerializeField] private GrinderScript grinder;

        public List<ShapeScript> GrinderShapes => grinder.Shapes;
        private readonly List<GameObject> _barrels = new List<GameObject>();
        private List<Goal>.Enumerator _goalEnumerator;
        public Goal CurrentGoal => _goalEnumerator.Current;

        private void Start()
        {
            EventManager.Instance.missed += GiveFeedbackAndReset;
            EventManager.Instance.sunk += NextScenario;

            _goalEnumerator = goals.GetEnumerator();
            if (_goalEnumerator.MoveNext()) SetupGoal();
        }

        public void SetupGoal()
        {
            GenerateBarrels();
            enemyShip.gameObject.SetActive(CurrentGoal.usesShip);
            FacilitatorScript.Instance.Say(CurrentGoal.FacilitatorMessage);
            UpdateInstructions();
            CurrentGoal.DisplayScreen();
        }

        private void GenerateBarrels()
        {
            foreach (var barrel in _barrels) Destroy(barrel);
            _barrels.Clear();
            
            var i = 0;
            foreach (var shape in CurrentGoal.Resources.OrderBy(value => Random.value))
            {
                var barrel = Instantiate(barrelPrefab, barrelArea.position, barrelArea.rotation, barrelArea);
                barrel.transform.Translate(Vector3.left * (1.5f * i), barrel.transform);
                barrel.GetComponent<BarrelScript>().Init(shape, CurrentGoal.FractionLabels);
                _barrels.Add(barrel);
                i++;
            }
        }

        private void UpdateInstructions()
        {
            InstructionsManager.Instance.SetText(CurrentGoal.GoalText());
        }

        public Outcome GoalsOutcome()
        {
            return CurrentGoal.GetOutcome();
        }

        private void GiveFeedbackAndReset()
        {
            CurrentGoal.GiveFeedback();
            CurrentGoal.ClearScreen();
            EventManager.Instance.reset?.Invoke();
        }

        private void NextScenario()
        {
            if (_goalEnumerator.MoveNext())
            {
                EventManager.Instance.reset?.Invoke();
                SetupGoal();
            }
            else
            {
                FacilitatorScript.Instance.Say("Congratulations! You finished the game.");
            }
        }
    }
}