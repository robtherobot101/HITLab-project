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
            EventManager.Instance.cannonFired += () => StartCoroutine(Bounce());
        }

        private IEnumerator Bounce()
        {
            _bouncing = true;
            var initialPos = hat.transform.position;
            var initialRot = hat.transform.rotation;
            //yield return Lerper.Lerp(hat.transform, initialPos + 0.5f * Vector3.up, initialRot * Quaternion.AngleAxis(180f, hat.transform.forward), 0.4f);
            yield return Lerper.Lerp(hat.transform, initialPos, initialRot * Quaternion.AngleAxis(359f, hat.transform.forward), 0.4f);
            _bouncing = false;
        }

        private void OnMouseDown()
        {
            if (!_bouncing) StartCoroutine(Bounce());
        }
    }
}