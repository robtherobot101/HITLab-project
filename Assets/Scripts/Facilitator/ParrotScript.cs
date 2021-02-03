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
            yield return Lerper.Lerp(hat.transform, initialPos + 0.5f * Vector3.up, initialRot * Quaternion.AngleAxis(180f, hat.transform.forward), 0.4f);
            yield return Lerper.Lerp(hat.transform, initialPos, initialRot * Quaternion.AngleAxis(359f, hat.transform.forward), 0.4f);
        }
    }
}