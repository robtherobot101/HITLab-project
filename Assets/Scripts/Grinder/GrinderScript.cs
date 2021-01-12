using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using Utils;

namespace Grinder
{
    public class GrinderScript : MonoBehaviour
    {
        private bool _grinding;
        private bool _ground;

        private GameObject _handle;
        private HeapScript _heap;
        private ParticleSystem _powder;

        public List<ShapeScript> Shapes { get; } = new List<ShapeScript>();

        // Start is called before the first frame update
        private void Start()
        {
            _powder = GetComponentInChildren<ParticleSystem>();
            _handle = GameObject.Find("Handle");
            _heap = GetComponentInChildren<HeapScript>();

            // EventManager.Instance.missed += Clear;
            EventManager.Instance.reset += Clear;
        }

        private void OnMouseDown()
        {
            if (_grinding || !GameManager.Instance.CurrentGoal.CanAdd()) return;

            var grindingObject = PlayerController.Instance.Take("Shape");
            if (grindingObject == null) return;

            Shapes.Add(grindingObject.GetComponent<ShapeScript>());
            GameManager.Instance.CurrentGoal.ShapeAdded(grindingObject.GetComponent<ShapeScript>());
        }

        private IEnumerator Grind()
        {
            _grinding = true;
            _powder.Play();
            StartCoroutine(_heap.Grow(Shapes.Aggregate(Fraction.Zero, (current, shape) => current + shape.Fraction)));
            float t = 0;
            while (t < 4)
            {
                _handle.transform.Rotate(0, -1, 0);
                t += Time.deltaTime;
                yield return null;
            }

            _powder.Stop();
            _grinding = false;
        }

        public void TurnHandle()
        {
            if (!_ground && GameManager.Instance.CurrentGoal.RequirementsSatisfied())
            {
                StartCoroutine(nameof(Grind));
                _ground = true;
            }
        }

        private void Clear()
        {
            _ground = false;
            Shapes.Clear();
        }
    }
}