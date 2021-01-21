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

        private void Start()
        {
            EventManager.Instance.cannonFired += () => StartCoroutine(Bounce());
        }

        public IEnumerator Bounce()
        {
            var initialPos = hat.transform.localPosition;
            var initialRot = hat.transform.localRotation;
            yield return Lerper.Lerp(hat.transform, initialPos + 0.05f * Vector3.up,
                Quaternion.FromToRotation(Vector3.left, Vector3.up), 0.15f);
            yield return Lerper.Lerp(hat.transform, initialPos, initialRot, 0.15f);
        }
    }
}