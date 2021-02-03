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

        private IEnumerator Bounce()
        {
            var initialPos = hat.transform.position;
            var initialRot = hat.transform.rotation;
            StartCoroutine(Flip());
            yield return Lerper.Lerp(hat.transform, initialPos + 0.05f * Vector3.up, initialRot, 0.15f);
            yield return Lerper.Lerp(hat.transform, initialPos, initialRot, 0.15f);
            StopCoroutine(Flip());
        }

        private IEnumerator Flip()
        {
            hat.transform.Rotate(0, 0, Time.deltaTime * 360);
            yield return null;
        }
    }
}