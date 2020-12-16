using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace Managers
{
    public class PlayerController : MonoSingleton<PlayerController>
    {
        private const float HoldDistance = 2f;
        private const float SpinFrequency = 0.5f;
        private Camera _camera;
        private GameObject _holding;
        private bool _isHolding;

        // Start is called before the first frame update
        private void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_isHolding)
            {
                _holding.transform.position =
                    _camera.ScreenToWorldPoint(Input.mousePosition + HoldDistance * Vector3.forward);
                _holding.transform.Rotate(Vector3.up, Time.deltaTime * 360 * SpinFrequency);
            }
        }


        public bool Give([CanBeNull] GameObject o)
        {
            if (_holding != null) return false;
            _holding = Instantiate(o);
            _holding.layer = 2;
            _isHolding = true;
            return true;
        }

        [CanBeNull]
        public GameObject Take(string ttag)
        {
            if (_holding == null || !_holding.CompareTag(ttag)) return null;

            Destroy(_holding);
            _isHolding = false;
            return _holding;
        }
    }
}