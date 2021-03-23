using System;
using System.Collections;
using Managers;
using UnityEngine;
using Utils;

namespace Facilitator
{
    public class ParrotScript : MonoBehaviour
    {
        [SerializeField] private GameObject hat;
        private bool _bouncing = false;

        private void Start()
        {
            Debug.Log("wat");
            EventManager.Instance.cannonFired += () => StartCoroutine(Bounce());
        }

        private IEnumerator Bounce()
        {
            _bouncing = true;
            var initialPos = hat.transform.position;
            var initialRot = hat.transform.rotation;
            StartCoroutine(Lerper.Lerp(hat.transform, 0.8f, 360f, hat.transform.up));
            yield return Lerper.Lerp(hat.transform, 0.4f, initialPos + 0.5f * Vector3.up);
            yield return Lerper.Lerp(hat.transform, 0.4f, initialPos);
            _bouncing = false;
        }

        private void OnMouseDown()
        {
            if (!_bouncing) StartCoroutine(Bounce());
        }
    }
}