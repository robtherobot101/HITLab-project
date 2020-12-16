using UnityEngine;

namespace Grinder
{
    public class HandleScript : MonoBehaviour
    {
        private GrinderScript _grinderScript;

        private void Start()
        {
            _grinderScript = GetComponentInParent<GrinderScript>();
        }

        private void OnMouseDown()
        {
            _grinderScript.TurnHandle();
        }
    }
}